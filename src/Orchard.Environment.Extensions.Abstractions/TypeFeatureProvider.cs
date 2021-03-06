﻿using System;
using System.Collections.Concurrent;
using Orchard.Environment.Extensions.Models;

namespace Orchard.Environment.Extensions
{
    /// <summary>
    /// An implementation of this service is able to provide the <see cref="Feature"/> that
    /// any services was harvested from.
    /// </summary>
    public interface ITypeFeatureProvider
    {
        Feature GetFeatureForDependency(Type dependency);
        void TryAdd(Type type, Feature feature);
    }

    public class TypeFeatureProvider : ITypeFeatureProvider
    {
        private readonly ConcurrentDictionary<Type, Feature> _features = new ConcurrentDictionary<Type, Feature>();

        public Feature GetFeatureForDependency(Type dependency)
        {
            Feature feature = null;

            if(_features.TryGetValue(dependency, out feature))
            {
                return feature;
            }

            return null;
        }

        public void TryAdd(Type type, Feature feature)
        {
            _features.TryAdd(type, feature);
        }
    }
}