/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ProzBevPassiv } from '../models/ProzBevPassiv';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class VerfahrenProzBevPassivService {
    /**
     * @param verfid
     * @returns ProzBevPassiv Success
     * @throws ApiError
     */
    public static getDatenVerfahrenProzbevpassiv(
        verfid: number,
    ): CancelablePromise<Array<ProzBevPassiv>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/prozbevpassiv',
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
     * @returns ProzBevPassiv Success
     * @throws ApiError
     */
    public static postDatenVerfahrenProzbevpassiv(
        verfid: number,
        prozBevId?: number,
        verfahrensId?: number,
        pb?: string,
    ): CancelablePromise<ProzBevPassiv> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/daten/verfahren/{verfid}/prozbevpassiv',
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
     * @returns ProzBevPassiv Success
     * @throws ApiError
     */
    public static getProzBevPassivById(
        verfid: number,
        id: number,
    ): CancelablePromise<ProzBevPassiv> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/prozbevpassiv/{id}',
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
    public static putDatenVerfahrenProzbevpassiv(
        verfid: number,
        id: number,
        prozBevId?: number,
        verfahrensId?: number,
        pb?: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/daten/verfahren/{verfid}/prozbevpassiv/{id}',
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
     * @returns ProzBevPassiv Success
     * @throws ApiError
     */
    public static deleteDatenVerfahrenProzbevpassiv(
        verfid: number,
        id: number,
    ): CancelablePromise<ProzBevPassiv> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/daten/verfahren/{verfid}/prozbevpassiv/{id}',
            path: {
                'verfid': verfid,
                'id': id,
            },
        });
    }
}
