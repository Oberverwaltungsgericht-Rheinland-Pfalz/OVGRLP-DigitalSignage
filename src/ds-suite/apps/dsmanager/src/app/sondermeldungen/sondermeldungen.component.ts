import { Component, OnInit } from '@angular/core';

import { NoteService } from '@ds-suite/core';

@Component({
  selector: 'sondermeldungen',
  templateUrl: './sondermeldungen.component.html',
  styleUrls: ['./sondermeldungen.component.css']
})
export class SondermeldungenComponent implements OnInit {

  constructor(private noteService: NoteService) { }
  public notes: any;
  public currentNote: any = null;

  loadNotes(){
    this.noteService.getNotesByBreeze().then(items => {
      this.notes=items;
      console.log("Notes:",this.notes);
    });
  }

  noteClick(note:any){
    this.noteService.breezeEntityManager.rejectChanges();
    this.currentNote=note;
    console.log(note)
  }

  cancelClick() {
    this.noteService.breezeEntityManager.rejectChanges();
    this.currentNote=null;
  }

  ngOnInit() {
    this.loadNotes();
    
  }

}
