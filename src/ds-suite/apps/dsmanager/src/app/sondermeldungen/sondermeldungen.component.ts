import { Component, OnInit, ViewChild } from '@angular/core';
import { DateFormatPipe } from 'angular2-moment';

import { CodemirrorComponent } from '@ctrl/ngx-codemirror';
import { YesNoDialogComponent } from '@ds-suite/ui';
import { NoteService } from '@ds-suite/core';
import { DisplayService } from '@ds-suite/core';
import { Display} from '@ds-suite/model';
import { NullAstVisitor } from '@angular/compiler';

@Component({
  selector: 'sondermeldungen',
  templateUrl: './sondermeldungen.component.html',
  styleUrls: ['./sondermeldungen.component.css']
})
export class SondermeldungenComponent implements OnInit {
  public notes: any;
  public displays: Display[];
  public currentDisplayNoteAssignments: NoteDisplayAssignment[] = [];
  public currentDisplayNoteAssignment: NoteDisplayAssignment = null;
  public currentDisplaysChecked: boolean[] = [] ;

  @ViewChild(YesNoDialogComponent) yesNoDialog: YesNoDialogComponent;
  @ViewChild('codemirrorEditor') codemirrorEditor: CodemirrorComponent;

  _currentNote: any = null;
  set currentNote (currentNote: any){
    this._currentNote = currentNote;
    this.loadCurrentNoteDisplayAssignments();
    this.currentDisplayNoteAssignment=null;
  };
  get currentNote(): any { return this._currentNote; }

  constructor(private noteService: NoteService,
    private displayService: DisplayService) { }

  loadDisplays(){
      this.displayService.getDisplays()
      .subscribe(
        displays => {
          this.displays = displays.sort((d1, d2) => d1.title > d2.title ? 1 : -1)
        },
        err => {
          console.error("Displays konnten nicht geladen werden: ",err);
        });
    }

  loadNotes(){
    this.noteService.getNotesByBreeze().then(items => {
      this.notes=items;
    });
  }

  noteClick(note:any){
    this.noteService.breezeEntityManager.rejectChanges();
    this.currentNote=note;
    this.setCodeMirrorSize();
  }

  editAssignmentClick(ass:NoteDisplayAssignment) {
    this.currentDisplayNoteAssignment=ass;
    this.currentDisplayNoteAssignment.Start=this.formatDate(this.currentDisplayNoteAssignment.Start,'YYYY-MM-DDThh:mm');
    this.currentDisplayNoteAssignment.End=this.formatDate(this.currentDisplayNoteAssignment.End,'YYYY-MM-DDThh:mm');

    this.initActivatedDisplays();

  }

  initActivatedDisplays(){
    var displaysChecked: boolean[] = [] ;
    var i:number;
    var active:boolean;

    for (i=0; i<this.displays.length; i++) {
      active=false;
      if (this.currentDisplayNoteAssignment.DisplayNames.findIndex(bez=> bez==this.displays[i].title)>=0){
        active=true;
      }
      displaysChecked.push(active);
    }
    this.currentDisplaysChecked=displaysChecked;
  }

  addNewClick() {
    this.noteService.breezeEntityManager.rejectChanges();
    this.currentNote = this.noteService.breezeEntityManager.createEntity('Note')
    this.currentNote.Content="";  //nullwert zur korrekten Anzeige verhindern
    this.setCodeMirrorSize();
  }

  addNewAssignmentClick() {
    var ass: NoteDisplayAssignment = {
      Id: [],
      DisplayId: [],
      DisplayNames: [],
      Start: null,
      StartForDisplay: null,
      End: null,
      EndForDisplay: null,
      Comment: ''
    };
    this.currentDisplayNoteAssignment=ass;
    this.initActivatedDisplays();
  }

  saveClick() {
    this.noteService.saveNotesByBreeze().then(() => {
      this.currentNote=null;
      this.loadNotes();
    });
  }

  saveAssignmentClick(){
    
    //!\TODO: Grafische Info anzeigen
    if (this.currentDisplaysChecked.filter(d => d==true).length==0) {
      return;
    }

    this.deleteCurrentAssignment();
    this.saveCurrentAssignment();
    this.currentDisplayNoteAssignment=null;
    this.loadCurrentNoteDisplayAssignments();
  }

  deleteClick(){
    this.yesNoDialog.open();
  }

  deleteAssignmentClick(){
    this.deleteCurrentAssignment()
    this.currentDisplayNoteAssignment=null;
    this.loadCurrentNoteDisplayAssignments();
  }

  saveCurrentAssignment() {
    var i:number;
    for (i=0; i<this.currentDisplaysChecked.length; i++) {
      if (this.currentDisplaysChecked[i]==true) {
        var displayId=this.displays[i].id;
        this.currentNote.entityAspect.entityManager.createEntity("NoteAssignment",{
          DisplayId: displayId, 
          Comment: this.currentDisplayNoteAssignment.Comment,
          Start: this.currentDisplayNoteAssignment.Start,
          End: this.currentDisplayNoteAssignment.End,
          NoteId: this.currentNote.Id
        })
      }
    }

  }

  setCodeMirrorSize() {
    // geht bestimmt besser - aber vorerst lauffähig
    var foo = new Promise<void>(resolve => {
      setTimeout(resolve, 100);
    }).then(() => {
      this.codemirrorEditor.codeMirror.setSize("100%","calc(100% - 16px)")
      this.codemirrorEditor.codeMirror.refresh();
    });
  }

  deleteCurrentAssignment() {
    var i:number;
    for (i=0; i<this.currentDisplayNoteAssignment.Id.length; i++) {
      var index=this.currentNote.NotesAssignments.findIndex(a=> a.Id==this.currentDisplayNoteAssignment.Id[i])
      this.currentNote.NotesAssignments[index].entityAspect.setDeleted();
    }
  }

  cancelClick() {
    this.noteService.breezeEntityManager.rejectChanges();
    this.currentNote=null;
  }

  cancelAssignmentClick(){
    this.currentDisplayNoteAssignment=null;
  }

  ngOnInit() {
    this.loadNotes();
    this.loadDisplays();
  }

  loadCurrentNoteDisplayAssignments() {
    var ass: NotesAssignments[] = null;
    var displayAss: NoteDisplayAssignment[] = [];
    
    if (null!=this.currentNote) {
      ass=this.currentNote.NotesAssignments;
      ass.forEach(a =>{
        var ind = displayAss.findIndex(d=> 
          this.formatDate(d.Start) == this.formatDate(a.Start) && 
          this.formatDate(d.End) == this.formatDate(a.End) && 
          d.Comment == a.Comment
          );
        var displayIndex=this.displays.findIndex(d=>d.id==a.DisplayId);
        if (ind>=0) {
          displayAss[ind].Id.push(a.Id);
          displayAss[ind].DisplayId.push(a.DisplayId);
          displayAss[ind].DisplayNames.push(this.displays[displayIndex].title);
        }
        else {
          var dsa: NoteDisplayAssignment ={
            Id: [a.Id],
            DisplayId: [a.DisplayId],
            DisplayNames: [this.displays[displayIndex].title],
            Start: a.Start, 
            StartForDisplay: this.formatDate(a.Start), 
            End: a.End, 
            EndForDisplay: this.formatDate(a.End), 
            Comment: a.Comment
          };
          displayAss.push(dsa)
        }
      });
    }
    
    this.currentDisplayNoteAssignments= displayAss;
  }

  OnDeleteResult(result:boolean) {
    var i:number;
    if (result) {
      
      //!\TODO: Löschen in einem Rutsch, inkl. Assignments implementieren
      //        Muss gehen, bei den Terminen und den Parteien ist es auch nichts anderes
      for (i=0; i<this.currentNote.NotesAssignments.length; i++) {
        this.currentNote.NotesAssignments[i].entityAspect.setDeleted();
      }
      this.noteService.saveNotesByBreeze().then(() => {
        this.noteService.deleteNoteByBreeze(this.currentNote).then(() => {
          this.currentNote=null;
          this.loadNotes();
        });
      });

    }
  }

formatDate(datetime:any,format:string ='DD.MM.YYYY hh:mm') {
  var rval:any = datetime;
  if (null!=datetime) {
    var df = new DateFormatPipe();
    rval=df.transform(datetime, format); 
  }
  return rval
 }

}

interface NotesAssignments {
  Id: number;
  DisplayId: number;
  Comment?: string;
  Start?: string;
  End?: string;
  NoteId: number;

}

interface NoteDisplayAssignment {
  Id: number[];
  DisplayId: number [];
  DisplayNames: string[];
  Start: any;
  StartForDisplay: any;
  End: any;
  EndForDisplay: any;
  Comment: string;
}