﻿// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.Settings;
using System.Reflection;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace DigitalSignage.WebApi.Services;

public class PermissionService
{
    private WindowsIdentity UserIdentity
    { get; set; }

    private List<string>? _securityMembers = null;

    public List<string> SecurityMembers
    {
        get
        {
            if (null == this._securityMembers)
            {
                this._securityMembers = new List<string>();
                this._securityMembers.Add(this.UserIdentity.Name);

                if(this.UserIdentity.Groups != null)
                    foreach (var group in this.UserIdentity.Groups)
                        this._securityMembers.Add(group.Translate(typeof(NTAccount)).ToString());        
            }
            return this._securityMembers;
        }
    }

    private List<Permission>? _permissions = null;

    public List<Permission> Permissions
    {
        get
        {
            if (null == this._permissions)
                this._permissions = _context.Permissions.ToList();
            
            return this._permissions;
        }
    }

    private readonly DigitalSignageDbContext _context;

    public PermissionService(WindowsIdentity userIdentity, DigitalSignageDbContext context)
    {
        this.UserIdentity = userIdentity;
        this._context = context;
    }

    public bool checkPermission(string urlPath, string httpMethod)
    {
        bool allowed = false;
        string[] methods = new[] { "GET", "PUT", "POST", "DELETE" };
        int i;
        List<Permission> perm = getSortedPermissions();
        var xx = this.SecurityMembers;

        for (i = 0; i < perm.Count; i++)
        {
            if (perm[i].Member == "*" || this.SecurityMembers.Contains(perm[i].Member))
            {
                if (CompareWithWildcards(urlPath, perm[i].Ressource))
                {
                    if (!methods.Contains(httpMethod))
                        httpMethod = "GET";
                    PropertyInfo? field = perm[i].GetType().GetProperty(httpMethod);
                    allowed = (bool) (field?.GetValue(perm[i]) ?? false);
                }
            }
        }
        System.Diagnostics.Trace.WriteLine("UrlRequest : " + urlPath + "(" + httpMethod + ")");

        return allowed;
    }

    public static bool CheckPermissionForIdentity(WindowsIdentity userIdentity, DigitalSignageDbContext context, string urlPath, string httpMethod)
    {
        var permService = new PermissionService(userIdentity, context);
        return permService.checkPermission(urlPath, httpMethod);
    }

    public List<Permission> getSortedPermissions()
    {
        List<Permission> perm;
        perm = this.Permissions.OrderBy(p => p.Ressource.Count(x => x == '/')).ThenBy(p => p.Id).ToList();
        return perm;
    }

    private bool CompareWithWildcards(string input, string mask)
    {
        return Regex.IsMatch(input, WildCardToRegular(mask));
    }

    private String WildCardToRegular(String value)
    {
        return "^" + Regex.Escape(value).Replace("\\*", ".*") + "$";
    }

    /*
    private bool CompareWildcard(string input, string mask)
    {
      CharEnumerator ie = input.GetEnumerator();
      CharEnumerator me = mask.GetEnumerator();
      return CompareWildcardInternal(ie, me);
    }

    private bool CompareWildcardInternal(CharEnumerator ie, CharEnumerator me)
    {
      CharEnumerator inputTryAhead;
      CharEnumerator maskTryAhead;
      while (me.MoveNext())
      {
        switch (me.Current)
        {
          case '?':
            if (!ie.MoveNext())
            {
              return false;
            }
            break;

          case '*': // non greedy match, first try * matches nothing
            do
            {
              inputTryAhead = (CharEnumerator)(ie.Clone());
              maskTryAhead = (CharEnumerator)(me.Clone());
              if (CompareWildcardInternal(inputTryAhead, maskTryAhead))
              {
                return true;
              }
            } while (ie.MoveNext());
            return false;

          case '\\':
            me.MoveNext();
            goto default;
          default:
            if (!ie.MoveNext() || ie.Current != me.Current)
              return false;
            break;
        }
      }

      return !ie.MoveNext();
    }

    */
}