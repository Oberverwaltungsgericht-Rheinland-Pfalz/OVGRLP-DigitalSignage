/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ProzBevAktiv } from '../models/ProzBevAktiv';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class VerfahrenProzBevAktivService {
    /**
     * @param verfid
     * @returns ProzBevAktiv Success
     * @throws ApiError
     */
    public static getDatenVerfahrenProzbevaktiv(
        verfid: number,
    ): CancelablePromise<Array<ProzBevAktiv>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/prozbevaktiv',
            path: {
                'verfid': verfid,
            },
        });
    }
    /**
     * @param verfid
     * @param prozBevId
     * @param verfahrensId
     * @param pb
     * @returns ProzBevAktiv Success
     * @throws ApiError
     */
    public static postDatenVerfahrenProzbevaktiv(
        verfid: number,
        prozBevId?: number,
        verfahrensId?: number,
        pb?: string,
    ): CancelablePromise<ProzBevAktiv> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/daten/verfahren/{verfid}/prozbevaktiv',
            path: {
                'verfid': verfid,
            },
            query: {
                'ProzBevId': prozBevId,
                'VerfahrensId': verfahrensId,
                'PB': pb,
            },
        });
    }
    /**
     * @param verfid
     * @param id
     * @returns ProzBevAktiv Success
     * @throws ApiError
     */
    public static getProzBevAktivById(
        verfid: number,
        id: number,
    ): CancelablePromise<ProzBevAktiv> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/prozbevaktiv/{id}',
            path: {
                'verfid': verfid,
                'id': id,
            },
        });
    }
    /**
     * @param verfid
     * @param id
     * @param prozBevId
     * @param verfahrensId
     * @param pb
     * @returns any Success
     * @throws ApiError
     */
    public static putDatenVerfahrenProzbevaktiv(
        verfid: number,
        id: number,
        prozBevId?: number,
        verfahrensId?: number,
        pb?: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/daten/verfahren/{verfid}/prozbevaktiv/{id}',
            path: {
                'verfid': verfid,
                'id': id,
            },
            query: {
                'ProzBevId': prozBevId,
                'VerfahrensId': verfahrensId,
                'PB': pb,
            },
        });
    }
    /**
     * @param verfid
     * @param id
     * @returns ProzBevAktiv Success
     * @throws ApiError
     */
    public static deleteDatenVerfahrenProzbevaktiv(
        verfid: number,
        id: number,
    ): CancelablePromise<ProzBevAktiv> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/daten/verfahren/{verfid}/prozbevaktiv/{id}',
            path: {
                'verfid': verfid,
                'id': id,
            },
        });
    }
}
