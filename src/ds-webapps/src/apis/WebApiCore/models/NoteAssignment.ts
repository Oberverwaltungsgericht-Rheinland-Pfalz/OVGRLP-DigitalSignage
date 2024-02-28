/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Display } from './Display';
import type { Note } from './Note';
export type NoteAssignment = {
    id?: number;
    note?: Note;
    noteId?: number;
    start?: string | null;
    end?: string | null;
    comment?: string | null;
    displayId?: number;
    display?: Display;
};

