/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ParteienSV } from '../models/ParteienSV';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class VerfahrenParteienSvService {
    /**
     * @param verfid
     * @returns ParteienSV Success
     * @throws ApiError
     */
    public static getDatenVerfahrenParteiensv(
        verfid: number,
    ): CancelablePromise<Array<ParteienSV>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/parteiensv',
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
     * @returns ParteienSV Success
     * @throws ApiError
     */
    public static postDatenVerfahrenParteiensv(
        verfid: number,
        parteiId?: number,
        verfahrensId?: number,
        partei?: string,
    ): CancelablePromise<ParteienSV> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/daten/verfahren/{verfid}/parteiensv',
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
     * @returns ParteienSV Success
     * @throws ApiError
     */
    public static getParteienSvById(
        verfid: number,
        id: number,
    ): CancelablePromise<ParteienSV> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/parteiensv/{id}',
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
    public static putDatenVerfahrenParteiensv(
        verfid: number,
        id: number,
        parteiId?: number,
        verfahrensId?: number,
        partei?: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/daten/verfahren/{verfid}/parteiensv/{id}',
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
     * @returns ParteienSV Success
     * @throws ApiError
     */
    public static deleteDatenVerfahrenParteiensv(
        verfid: number,
        id: number,
    ): CancelablePromise<ParteienSV> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/daten/verfahren/{verfid}/parteiensv/{id}',
            path: {
                'verfid': verfid,
                'id': id,
            },
        });
    }
}
