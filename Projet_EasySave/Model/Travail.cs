//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Projet_EasySave.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Travail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SaveType { get; set; }
        public Nullable<System.DateTime> TimeStamping { get; set; }
        public string TaskName { get; set; }
        public string SourceFile { get; set; }
        public string DestinationFile { get; set; }
        public Nullable<int> FileSize { get; set; }
        public Nullable<int> TransferTime { get; set; }
    }
}