/* generated using openapi-typescript-codegen -- do no edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { BasicPermissions } from '../models/BasicPermissions';
import type { Permission } from '../models/Permission';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class PermissionsService {
    /**
     * @returns Permission Success
     * @throws ApiError
     */
    public static getSettingsPermissions(): CancelablePromise<Array<Permission>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/permissions',
        });
    }
    /**
     * @returns string Success
     * @throws ApiError
     */
    public static getSettingsPermissionsCurrentUserMembers(): CancelablePromise<Array<string>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/permissions/CurrentUserMembers',
        });
    }
    /**
     * @returns BasicPermissions Success
     * @throws ApiError
     */
    public static getSettingsPermissionsBasicPermissions(): CancelablePromise<BasicPermissions> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/permissions/BasicPermissions',
        });
    }
    /**
     * @param urlPath
     * @returns boolean Success
     * @throws ApiError
     */
    public static getSettingsPermissionsGetPermission(
        urlPath?: string,
    ): CancelablePromise<boolean> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/permissions/GetPermission',
            query: {
                'urlPath': urlPath,
            },
        });
    }
    /**
     * @param urlPath
     * @returns any Success
     * @throws ApiError
     */
    public static getSettingsPermissionsPutPermission(
        urlPath?: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/permissions/PutPermission',
            query: {
                'urlPath': urlPath,
            },
        });
    }
    /**
     * @param urlPath
     * @returns any Success
     * @throws ApiError
     */
    public static getSettingsPermissionsPostPermission(
        urlPath?: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/permissions/PostPermission',
            query: {
                'urlPath': urlPath,
            },
        });
    }
    /**
     * @param urlPath
     * @returns any Success
     * @throws ApiError
     */
    public static getSettingsPermissionsDeletePermission(
        urlPath?: string,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/settings/permissions/DeletePermission',
            query: {
                'urlPath': urlPath,
            },
        });
    }
}
