/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { NoteAssignment } from './NoteAssignment';
export type DisplayDto = {
    id?: number;
    name?: string | null;
    title?: string | null;
    template?: string | null;
    styles?: string | null;
    filter?: string | null;
    group?: string | null;
    controlUrl?: string | null;
    netAddress?: string | null;
    wolIpAddress?: string | null;
    wolMacAddress?: string | null;
    wolUdpPort?: number;
    description?: string | null;
    notesAssignments?: Array<NoteAssignment> | null;
    dummy?: boolean;
};

