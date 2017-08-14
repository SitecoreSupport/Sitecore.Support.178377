using Sitecore.Analytics;
using Sitecore.Analytics.Outcome;
using Sitecore.Analytics.Outcome.Extensions;
using Sitecore.Analytics.Outcome.Model;
using Sitecore.Analytics.Tracking;
using Sitecore.Common;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Support;

namespace Sitecore.Support.Analytics.Outcome.Rules
{
  public class ContactHasRegisteredOutcomeDuringAnyInteractionCondition<T> : WhenCondition<T> where T : RuleContext
  {
    // Methods
    protected override bool Execute(T ruleContext)
    {
      Guid guid;
      Assert.IsNotNull(Tracker.Current, "Tracker.Current is not initialized");
      Assert.IsNotNull(Tracker.Current.Session, "Tracker.Current.Session is not initialized");
      Assert.IsNotNull(Tracker.Current.Session.Interaction, "Tracker.Current.Session.Interaction is not initialized");
      Assert.ArgumentNotNull(ruleContext, "ruleContext");
      if(!Guid.TryParse(this.OutcomeDefinition, out guid))
      {
        Log.Debug($"Specified outcome [{this.OutcomeDefinition}] was not a valid Guid");
        return false;
      }
      bool flag = OutcomeManager.DefaultManager.HasOutcome(Tracker.Current.Interaction.ContactId.ToID(), guid.ToID());

      return flag;
    }
    // Properties
    public string OutcomeDefinition { get; set; }
  }
}