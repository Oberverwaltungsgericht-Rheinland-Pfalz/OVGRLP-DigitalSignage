/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Stammdaten } from '../models/Stammdaten';
import type { Verfahren } from '../models/Verfahren';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class StammdatenService {
    /**
     * @returns Stammdaten Success
     * @throws ApiError
     */
    public static getDatenStammdaten(): CancelablePromise<Array<Stammdaten>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/stammdaten',
        });
    }
    /**
     * @param stammdatenId
     * @param gerichtsname
     * @param datum
     * @param verfahren
     * @returns Stammdaten Success
     * @throws ApiError
     */
    public static postDatenStammdaten(
        stammdatenId?: number,
        gerichtsname?: string,
        datum?: string,
        verfahren?: Array<Verfahren>,
    ): CancelablePromise<Stammdaten> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/daten/stammdaten',
            query: {
                'StammdatenId': stammdatenId,
                'Gerichtsname': gerichtsname,
                'Datum': datum,
                'Verfahren': verfahren,
            },
        });
    }
    /**
     * @param id
     * @returns Stammdaten Success
     * @throws ApiError
     */
    public static getStammdatenById(
        id: number,
    ): CancelablePromise<Stammdaten> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/stammdaten/{id}',
            path: {
                'id': id,
            },
        });
    }
    /**
     * @param id
     * @param stammdatenId
     * @param gerichtsname
     * @param datum
     * @param verfahren
     * @returns any Success
     * @throws ApiError
     */
    public static putDatenStammdaten(
        id: number,
        stammdatenId?: number,
        gerichtsname?: string,
        datum?: string,
        verfahren?: Array<Verfahren>,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/daten/stammdaten/{id}',
            path: {
                'id': id,
            },
            query: {
                'StammdatenId': stammdatenId,
                'Gerichtsname': gerichtsname,
                'Datum': datum,
                'Verfahren': verfahren,
            },
        });
    }
    /**
     * @param id
     * @returns Stammdaten Success
     * @throws ApiError
     */
    public static deleteDatenStammdaten(
        id: number,
    ): CancelablePromise<Stammdaten> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/daten/stammdaten/{id}',
            path: {
                'id': id,
            },
        });
    }
}
