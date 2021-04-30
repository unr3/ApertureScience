using ApertureScience.AccelerometerIngest.Domain.Entities;
using ApertureScience.Web.ApiGateway.Event.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApertureScience.AccelerometerIngest.Function
{
   public  static class IngestViewModelToIngestEntitiy
    {
      public static Ingest[] From(IngestDataViewModel[] viewModel)
        {
            Ingest[] data = new Ingest[viewModel.Length];
            for (int i = 0; i < data.Length; i++)
                data[i] = new Ingest(Guid.NewGuid(),viewModel[i].UserId, viewModel[i].DimensionX, viewModel[i].DimensionY, viewModel[i].DimensionZ, viewModel[i].TimeStamp);
            return data;
         

        }
    }
}
