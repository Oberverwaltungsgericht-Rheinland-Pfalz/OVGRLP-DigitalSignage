/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { NoteAssignment } from './NoteAssignment';
export type Note = {
    id?: number;
    name: string;
    content?: string | null;
    forced: boolean;
    notesAssignments?: Array<NoteAssignment> | null;
};

