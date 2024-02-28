/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Besetzung } from '../models/Besetzung';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class VerfahrenBesetzungService {
    /**
     * @param verfid
     * @returns Besetzung Success
     * @throws ApiError
     */
    public static getDatenVerfahrenBesetzung(
        verfid: number,
    ): CancelablePromise<Array<Besetzung>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/besetzung',
            path: {
                'verfid': verfid,
            },
        });
    }
    /**
     * @param verfid
     * @param besetzungsId
     * @param verfahrensId
     * @param richter
     * @returns Besetzung Success
     * @throws ApiError
     */
    public static postDatenVerfahrenBesetzung(
        verfid: number,
        besetzungsId?: number,
        verfahrensId?: number,
        richter?: string,
    ): CancelablePromise<Besetzung> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/daten/verfahren/{verfid}/besetzung',
            path: {
                'verfid': verfid,
            },
            query: {
                'BesetzungsId': besetzungsId,
                'VerfahrensId': verfahrensId,
                'Richter': richter,
            },
        });
    }
    /**
     * @param verfid
     * @param id
     * @returns Besetzung Success
     * @throws ApiError
     */
    public static getBesetzungById(
        verfid: number,
        id: number,
    ): CancelablePromise<Besetzung> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/daten/verfahren/{verfid}/besetzung/{id}',
            path: {
                'verfid': verfid,
                'id': id,
            },
        });
    }
    /**
     * @param verfid
     * @param id
     * @param besetzungsId
     * @param verfahrensId
     * @param richter
     * @returns any Success
     * @throws ApiError
     */
    public static putDatenVerfahrenBesetzung(
        verfid: number,
        id: number,
        besetzungsId?: number,
        verfahrensId?: number,
        richter?: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/daten/verfahren/{verfid}/besetzung/{id}',
            path: {
                'verfid': verfid,
                'id': id,
            },
            query: {
                'BesetzungsId': besetzungsId,
                'VerfahrensId': verfahrensId,
                'Richter': richter,
            },
        });
    }
    /**
     * @param verfid
     * @param id
     * @returns Besetzung Success
     * @throws ApiError
     */
    public static deleteDatenVerfahrenBesetzung(
        verfid: number,
        id: number,
    ): CancelablePromise<Besetzung> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/daten/verfahren/{verfid}/besetzung/{id}',
            path: {
                'verfid': verfid,
                'id': id,
            },
        });
    }
}
