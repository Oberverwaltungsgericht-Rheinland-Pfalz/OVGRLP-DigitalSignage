// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Breeze.Persistence.EFCore;
using DigitalSignage.Data;

namespace DigitalSignage.dn.WebApiCore.Services;

public class DigitalSignagePersistenceManager : EFPersistenceManager<DigitalSignageDbContext>
{
    public DigitalSignagePersistenceManager(DigitalSignageDbContext dbContext): base(dbContext) { }
}