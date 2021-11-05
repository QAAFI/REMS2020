﻿using Models.Core;
using System;
using System.Threading.Tasks;

namespace Rems.Application.Common.Interfaces
{
    /// <summary>
    /// A template for creating an instance of a class
    /// </summary>
    public interface ITemplate
    {
        public IModel Create();

        public Task<IModel> AsyncCreate();
    }
}