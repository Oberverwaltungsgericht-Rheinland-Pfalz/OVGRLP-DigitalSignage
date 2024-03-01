/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { NoteAssignment } from './NoteAssignment';
export type DisplayDto = {
    id: number;
    name: string;
    title: string;
    template: string;
    styles?: string | null;
    filter?: string | null;
    group: string;
    controlUrl?: string | null;
    netAddress: string;
    wolIpAddress: string;
    wolMacAddress: string;
    wolUdpPort: number;
    description: string;
    notesAssignments: Array<NoteAssignment>;
    dummy: boolean;
};

