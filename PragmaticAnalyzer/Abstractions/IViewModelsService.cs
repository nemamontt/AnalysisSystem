﻿using PragmaticAnalyzer.MVVM.ViewModel;
using PragmaticAnalyzer.MVVM.ViewModel.Viewer;

namespace PragmaticAnalyzer.Abstractions
{
    public interface IViewModelsService
    {
        ThreatViewModel ThreatVm { get; }
        VulnerabilitieViewModel VulnerabilitieVm { get; }
        ExploitViewModel ExploitVm { get; }
        OntologyViewModel OntologyVm { get; }
        TacticViewModel TacticVm { get; }
        ViolatorViewModel ViolatorVm { get; }
        ProtectionMeasureViewModel ProtectionMeasureVm { get; }
        SpecialistViewModel SpecialistVm { get; }
        ReferenceStatusViewModel ReferenceStatusVm { get; }
        CurrentStatusViewModel CurrentStatusVm { get; }
        OutcomesViewModel OutcomeVm { get; }
        SetViewModel SetVm { get; }
        ViewerViewModel ViewerVm { get; }
        ConnectionViewModel ConnectionVm { get; }
        HelpViewModel HelpVm { get; }
    }
}