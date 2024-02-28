/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Verfahren } from '../models/Verfahren';
import type { VerfahrenDto } from '../models/VerfahrenDto';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class VerfahrenService {
    /**
     * @returns VerfahrenDto Success
     * @throws ApiError
     */
    public static getDatenVerfahren(): CancelablePromise<Array<VerfahrenDto>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren',
        });
    }
    /**
     * @param requestBody
     * @returns VerfahrenDto Success
     * @throws ApiError
     */
    public static postDatenVerfahren(
        requestBody?: VerfahrenDto,
    ): CancelablePromise<VerfahrenDto> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/daten/verfahren',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @param id
     * @returns VerfahrenDto Success
     * @throws ApiError
     */
    public static getVerfahrenById(
        id: number,
    ): CancelablePromise<VerfahrenDto> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{id}',
            path: {
                'id': id,
            },
        });
    }
    /**
     * @param id
     * @param requestBody
     * @returns any Success
     * @throws ApiError
     */
    public static putDatenVerfahren(
        id: number,
        requestBody?: VerfahrenDto,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/daten/verfahren/{id}',
            path: {
                'id': id,
            },
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @param id
     * @returns Verfahren Success
     * @throws ApiError
     */
    public static deleteDatenVerfahren(
        id: number,
    ): CancelablePromise<Verfahren> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/daten/verfahren/{id}',
            path: {
                'id': id,
            },
        });
    }
}
