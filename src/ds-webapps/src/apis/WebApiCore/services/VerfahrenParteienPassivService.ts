/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ParteienPassiv } from '../models/ParteienPassiv';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class VerfahrenParteienPassivService {
    /**
     * @param verfid
     * @returns ParteienPassiv Success
     * @throws ApiError
     */
    public static getDatenVerfahrenParteienpassiv(
        verfid: number,
    ): CancelablePromise<Array<ParteienPassiv>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/parteienpassiv',
            path: {
                'verfid': verfid,
            },
        });
    }
    /**
     * @param verfid
     * @param parteiId
     * @param verfahrensId
     * @param partei
     * @returns ParteienPassiv Success
     * @throws ApiError
     */
    public static postDatenVerfahrenParteienpassiv(
        verfid: number,
        parteiId?: number,
        verfahrensId?: number,
        partei?: string,
    ): CancelablePromise<ParteienPassiv> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/daten/verfahren/{verfid}/parteienpassiv',
            path: {
                'verfid': verfid,
            },
            query: {
                'ParteiId': parteiId,
                'VerfahrensId': verfahrensId,
                'Partei': partei,
            },
        });
    }
    /**
     * @param verfid
     * @param id
     * @returns ParteienPassiv Success
     * @throws ApiError
     */
    public static getParteienPassivById(
        verfid: number,
        id: number,
    ): CancelablePromise<ParteienPassiv> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/parteienpassiv/{id}',
            path: {
                'verfid': verfid,
                'id': id,
            },
        });
    }
    /**
     * @param verfid
     * @param id
     * @param parteiId
     * @param verfahrensId
     * @param partei
     * @returns any Success
     * @throws ApiError
     */
    public static putDatenVerfahrenParteienpassiv(
        verfid: number,
        id: number,
        parteiId?: number,
        verfahrensId?: number,
        partei?: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/daten/verfahren/{verfid}/parteienpassiv/{id}',
            path: {
                'verfid': verfid,
                'id': id,
            },
            query: {
                'ParteiId': parteiId,
                'VerfahrensId': verfahrensId,
                'Partei': partei,
            },
        });
    }
    /**
     * @param verfid
     * @param id
     * @returns ParteienPassiv Success
     * @throws ApiError
     */
    public static deleteDatenVerfahrenParteienpassiv(
        verfid: number,
        id: number,
    ): CancelablePromise<ParteienPassiv> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/daten/verfahren/{verfid}/parteienpassiv/{id}',
            path: {
                'verfid': verfid,
                'id': id,
            },
        });
    }
}
