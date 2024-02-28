/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ParteienBeigeladen } from '../models/ParteienBeigeladen';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class VerfahrenParteienBeigeladenService {
    /**
     * @param verfid
     * @returns ParteienBeigeladen Success
     * @throws ApiError
     */
    public static getDatenVerfahrenParteienbeigeladen(
        verfid: number,
    ): CancelablePromise<Array<ParteienBeigeladen>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/parteienbeigeladen',
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
     * @returns ParteienBeigeladen Success
     * @throws ApiError
     */
    public static postDatenVerfahrenParteienbeigeladen(
        verfid: number,
        parteiId?: number,
        verfahrensId?: number,
        partei?: string,
    ): CancelablePromise<ParteienBeigeladen> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/daten/verfahren/{verfid}/parteienbeigeladen',
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
     * @returns ParteienBeigeladen Success
     * @throws ApiError
     */
    public static getParteienBeigeladenById(
        verfid: number,
        id: number,
    ): CancelablePromise<ParteienBeigeladen> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/parteienbeigeladen/{id}',
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
    public static putDatenVerfahrenParteienbeigeladen(
        verfid: number,
        id: number,
        parteiId?: number,
        verfahrensId?: number,
        partei?: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/daten/verfahren/{verfid}/parteienbeigeladen/{id}',
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
     * @returns ParteienBeigeladen Success
     * @throws ApiError
     */
    public static deleteDatenVerfahrenParteienbeigeladen(
        verfid: number,
        id: number,
    ): CancelablePromise<ParteienBeigeladen> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/daten/verfahren/{verfid}/parteienbeigeladen/{id}',
            path: {
                'verfid': verfid,
                'id': id,
            },
        });
    }
}
