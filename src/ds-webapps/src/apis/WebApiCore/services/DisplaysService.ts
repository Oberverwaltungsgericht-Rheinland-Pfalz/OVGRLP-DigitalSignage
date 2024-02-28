/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Display } from '../models/Display';
import type { DisplayDto } from '../models/DisplayDto';
import type { DisplayStatus } from '../models/DisplayStatus';
import type { Note } from '../models/Note';
import type { VerfahrenDto } from '../models/VerfahrenDto';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class DisplaysService {
    /**
     * @returns Display Success
     * @throws ApiError
     */
    public static getSettingsDisplays(): CancelablePromise<Array<Display>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/displays',
        });
    }
    /**
     * @param requestBody
     * @returns any Success
     * @throws ApiError
     */
    public static postSettingsDisplays(
        requestBody?: Display,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/settings/displays',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @returns DisplayDto Success
     * @throws ApiError
     */
    public static getSettingsDisplaysDisplaysEx(): CancelablePromise<Array<DisplayDto>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/displays/DisplaysEx',
        });
    }
    /**
     * @param name
     * @returns any Success
     * @throws ApiError
     */
    public static getDisplay(
        name: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/displays/{name}',
            path: {
                'name': name,
            },
        });
    }
    /**
     * @param name
     * @param requestBody
     * @returns any Success
     * @throws ApiError
     */
    public static putSettingsDisplays(
        name: string,
        requestBody?: Display,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/settings/displays/{name}',
            path: {
                'name': name,
            },
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @param name
     * @returns Display Success
     * @throws ApiError
     */
    public static deleteSettingsDisplays(
        name: string,
    ): CancelablePromise<Display> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/settings/displays/{name}',
            path: {
                'name': name,
            },
        });
    }
    /**
     * @param name
     * @returns DisplayDto Success
     * @throws ApiError
     */
    public static getDisplayEx(
        name: string,
    ): CancelablePromise<DisplayDto> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/displays/{name}/DisplayEx',
            path: {
                'name': name,
            },
        });
    }
    /**
     * @param name
     * @returns VerfahrenDto Success
     * @throws ApiError
     */
    public static getSettingsDisplaysTermine(
        name: string,
    ): CancelablePromise<Array<VerfahrenDto>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/displays/{name}/termine',
            path: {
                'name': name,
            },
        });
    }
    /**
     * @param name
     * @param timestamp
     * @returns Note Success
     * @throws ApiError
     */
    public static getSettingsDisplaysActivenotes(
        name: string,
        timestamp?: string,
    ): CancelablePromise<Array<Note>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/displays/{name}/activenotes',
            path: {
                'name': name,
            },
            query: {
                'timestamp': timestamp,
            },
        });
    }
    /**
     * @param name
     * @returns DisplayStatus Success
     * @throws ApiError
     */
    public static getSettingsDisplaysStatus(
        name: string,
    ): CancelablePromise<DisplayStatus> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/displays/{name}/status',
            path: {
                'name': name,
            },
        });
    }
    /**
     * @param name
     * @returns any Success
     * @throws ApiError
     */
    public static getSettingsDisplaysStart(
        name: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/displays/{name}/start',
            path: {
                'name': name,
            },
        });
    }
    /**
     * @param name
     * @returns any Success
     * @throws ApiError
     */
    public static getSettingsDisplaysRestart(
        name: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/displays/{name}/restart',
            path: {
                'name': name,
            },
        });
    }
    /**
     * @param name
     * @returns any Success
     * @throws ApiError
     */
    public static getSettingsDisplaysStop(
        name: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/displays/{name}/stop',
            path: {
                'name': name,
            },
        });
    }
    /**
     * @param name
     * @returns any Success
     * @throws ApiError
     */
    public static getSettingsDisplaysScreenshotUrl(
        name: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/displays/{name}/ScreenshotUrl',
            path: {
                'name': name,
            },
        });
    }
}
