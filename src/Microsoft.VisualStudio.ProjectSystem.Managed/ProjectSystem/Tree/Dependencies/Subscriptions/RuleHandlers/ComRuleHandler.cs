﻿// Licensed to the .NET Foundation under one or more agreements. The .NET Foundation licenses this file to you under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.Immutable;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudio.ProjectSystem.Tree.Dependencies.CrossTarget;
using Microsoft.VisualStudio.ProjectSystem.Tree.Dependencies.Models;
using Microsoft.VisualStudio.ProjectSystem.VS;
using Microsoft.VisualStudio.ProjectSystem.VS.Tree.Dependencies;

namespace Microsoft.VisualStudio.ProjectSystem.Tree.Dependencies.Subscriptions.RuleHandlers
{
    [Export(DependencyRulesSubscriber.DependencyRulesSubscriberContract, typeof(IDependenciesRuleHandler))]
    [Export(typeof(IProjectDependenciesSubTreeProvider))]
    [AppliesTo(ProjectCapability.DependenciesTree)]
    internal class ComRuleHandler : DependenciesRuleHandlerBase
    {
        public const string ProviderTypeString = "ComDependency";

        private static readonly DependencyGroupModel s_groupModel = new DependencyGroupModel(
            ProviderTypeString,
            Resources.ComNodeName,
            new DependencyIconSet(
                icon: ManagedImageMonikers.Component,
                expandedIcon: ManagedImageMonikers.Component,
                unresolvedIcon: ManagedImageMonikers.ComponentWarning,
                unresolvedExpandedIcon: ManagedImageMonikers.ComponentWarning),
            DependencyTreeFlags.ComDependencyGroup);

        public ComRuleHandler()
            : base(ComReference.SchemaName, ResolvedCOMReference.SchemaName)
        {
        }

        public override string ProviderType => ProviderTypeString;

        public override ImageMoniker ImplicitIcon => ManagedImageMonikers.ComponentPrivate;

        public override IDependencyModel CreateRootDependencyNode() => s_groupModel;

        protected override IDependencyModel CreateDependencyModel(
            string path,
            string originalItemSpec,
            bool resolved,
            bool isImplicit,
            IImmutableDictionary<string, string> properties)
        {
            return new ComDependencyModel(
                path,
                originalItemSpec,
                resolved,
                isImplicit,
                properties);
        }
    }
}