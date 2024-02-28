/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ParteienZeugen } from '../models/ParteienZeugen';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class VerfahrenParteienZeugenService {
    /**
     * @param verfid
     * @returns ParteienZeugen Success
     * @throws ApiError
     */
    public static getDatenVerfahrenParteienzeugen(
        verfid: number,
    ): CancelablePromise<Array<ParteienZeugen>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/parteienzeugen',
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
     * @returns ParteienZeugen Success
     * @throws ApiError
     */
    public static postDatenVerfahrenParteienzeugen(
        verfid: number,
        parteiId?: number,
        verfahrensId?: number,
        partei?: string,
    ): CancelablePromise<ParteienZeugen> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/daten/verfahren/{verfid}/parteienzeugen',
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
     * @returns ParteienZeugen Success
     * @throws ApiError
     */
    public static getParteienZeugenById(
        verfid: number,
        id: number,
    ): CancelablePromise<ParteienZeugen> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/parteienzeugen/{id}',
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
    public static putDatenVerfahrenParteienzeugen(
        verfid: number,
        id: number,
        parteiId?: number,
        verfahrensId?: number,
        partei?: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/daten/verfahren/{verfid}/parteienzeugen/{id}',
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
     * @returns ParteienZeugen Success
     * @throws ApiError
     */
    public static deleteDatenVerfahrenParteienzeugen(
        verfid: number,
        id: number,
    ): CancelablePromise<ParteienZeugen> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/daten/verfahren/{verfid}/parteienzeugen/{id}',
            path: {
                'verfid': verfid,
                'id': id,
            },
        });
    }
}
