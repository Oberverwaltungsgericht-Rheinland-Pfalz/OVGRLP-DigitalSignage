/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ProzBevBeigeladen } from '../models/ProzBevBeigeladen';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class VerfahrenProzBevBeigeladenService {
    /**
     * @param verfid
     * @returns ProzBevBeigeladen Success
     * @throws ApiError
     */
    public static getDatenVerfahrenProzbevbeigeladen(
        verfid: number,
    ): CancelablePromise<Array<ProzBevBeigeladen>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/prozbevbeigeladen',
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
     * @returns ProzBevBeigeladen Success
     * @throws ApiError
     */
    public static postDatenVerfahrenProzbevbeigeladen(
        verfid: number,
        prozBevId?: number,
        verfahrensId?: number,
        pb?: string,
    ): CancelablePromise<ProzBevBeigeladen> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/daten/verfahren/{verfid}/prozbevbeigeladen',
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
     * @returns ProzBevBeigeladen Success
     * @throws ApiError
     */
    public static getProzBevBeigeladenById(
        verfid: number,
        id: number,
    ): CancelablePromise<ProzBevBeigeladen> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/prozbevbeigeladen/{id}',
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
    public static putDatenVerfahrenProzbevbeigeladen(
        verfid: number,
        id: number,
        prozBevId?: number,
        verfahrensId?: number,
        pb?: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/daten/verfahren/{verfid}/prozbevbeigeladen/{id}',
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
     * @returns ProzBevBeigeladen Success
     * @throws ApiError
     */
    public static deleteDatenVerfahrenProzbevbeigeladen(
        verfid: number,
        id: number,
    ): CancelablePromise<ProzBevBeigeladen> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/daten/verfahren/{verfid}/prozbevbeigeladen/{id}',
            path: {
                'verfid': verfid,
                'id': id,
            },
        });
    }
}
