/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ParteienAktiv } from '../models/ParteienAktiv';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class VerfahrenParteienAktivService {
    /**
     * @param verfid
     * @returns ParteienAktiv Success
     * @throws ApiError
     */
    public static getDatenVerfahrenParteienaktiv(
        verfid: number,
    ): CancelablePromise<Array<ParteienAktiv>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/parteienaktiv',
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
     * @returns ParteienAktiv Success
     * @throws ApiError
     */
    public static postDatenVerfahrenParteienaktiv(
        verfid: number,
        parteiId?: number,
        verfahrensId?: number,
        partei?: string,
    ): CancelablePromise<ParteienAktiv> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/daten/verfahren/{verfid}/parteienaktiv',
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
     * @returns ParteienAktiv Success
     * @throws ApiError
     */
    public static getParteienAktivById(
        verfid: number,
        id: number,
    ): CancelablePromise<ParteienAktiv> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/parteienaktiv/{id}',
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
    public static putDatenVerfahrenParteienaktiv(
        verfid: number,
        id: number,
        parteiId?: number,
        verfahrensId?: number,
        partei?: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/daten/verfahren/{verfid}/parteienaktiv/{id}',
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
     * @returns ParteienAktiv Success
     * @throws ApiError
     */
    public static deleteDatenVerfahrenParteienaktiv(
        verfid: number,
        id: number,
    ): CancelablePromise<ParteienAktiv> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/daten/verfahren/{verfid}/parteienaktiv/{id}',
            path: {
                'verfid': verfid,
                'id': id,
            },
        });
    }
}
