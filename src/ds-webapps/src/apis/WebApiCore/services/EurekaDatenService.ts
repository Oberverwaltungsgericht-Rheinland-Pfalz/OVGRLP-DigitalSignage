/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { JToken } from '../models/JToken';
import type { Note } from '../models/Note';
import type { SaveResult } from '../models/SaveResult';
import type { Verfahren } from '../models/Verfahren';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class EurekaDatenService {
    /**
     * @returns string Success
     * @throws ApiError
     */
    public static getBreezeEurekaDatenMetadata(): CancelablePromise<string> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/breeze/EurekaDaten/Metadata',
        });
    }
    /**
     * @returns Verfahren Success
     * @throws ApiError
     */
    public static getBreezeEurekaDatenVerfahren(): CancelablePromise<Array<Verfahren>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/breeze/EurekaDaten/Verfahren',
        });
    }
    /**
     * @returns any Success
     * @throws ApiError
     */
    public static getBreezeEurekaDatenVerfahrenList(): CancelablePromise<Array<any>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/breeze/EurekaDaten/VerfahrenList',
        });
    }
    /**
     * @returns any Success
     * @throws ApiError
     */
    public static getBreezeEurekaDatenDisplays(): CancelablePromise<Array<any>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/breeze/EurekaDaten/Displays',
        });
    }
    /**
     * @returns Note Success
     * @throws ApiError
     */
    public static getBreezeEurekaDatenNotes(): CancelablePromise<Array<Note>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/breeze/EurekaDaten/Notes',
        });
    }
    /**
     * @param id
     * @returns any Success
     * @throws ApiError
     */
    public static getBreezeEurekaDatenDisplayStatusDisplayStatus(
        id: number,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/breeze/EurekaDaten/DisplayStatus/Display/{id}/status',
            path: {
                'id': id,
            },
        });
    }
    /**
     * @param id
     * @returns any Success
     * @throws ApiError
     */
    public static getBreezeEurekaDatenDisplayPowerOnDisplayPoweron(
        id: number,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/breeze/EurekaDaten/DisplayPowerOn/Display/{id}/poweron',
            path: {
                'id': id,
            },
        });
    }
    /**
     * @param saveBundle
     * @returns SaveResult Success
     * @throws ApiError
     */
    public static postBreezeEurekaDatenSaveChanges(
        saveBundle?: Record<string, JToken>,
    ): CancelablePromise<SaveResult> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/breeze/EurekaDaten/SaveChanges',
            query: {
                'saveBundle': saveBundle,
            },
        });
    }
}
