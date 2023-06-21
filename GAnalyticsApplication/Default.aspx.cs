using Google.Analytics.Data.V1Beta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAnalyticsApplication
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SampleRunReport();
            }
        }

        public string SampleRunReport(string propertyId = "YOUR-GA4-PROPERTY-ID", string credentialsJsonPath = "")
        {
            /**
             * TODO(developer): Uncomment this variable and replace with your
             *  Google Analytics 4 property ID before running the sample.
             */
            //propertyId = "379762242";
            propertyId = "298605434";

            // [START analyticsdata_json_credentials_initialize]
            /**
             * TODO(developer): Uncomment this variable and replace with a valid path to
             *  the credentials.json file for your service account downloaded from the
             *  Cloud Console.
             *  Otherwise, default service account credentials will be derived from
             *  the GOOGLE_APPLICATION_CREDENTIALS environment variable.
             */
            //credentialsJsonPath = "ganalyticscapp-ad73fa6d5292.json";
            credentialsJsonPath = Server.MapPath("~\\1.json");
            

            // Explicitly use service account credentials by specifying
            // the private key file.
            BetaAnalyticsDataClient client = new BetaAnalyticsDataClientBuilder()
            {
                CredentialsPath = credentialsJsonPath
            }.Build();
            // [END analyticsdata_json_credentials_initialize]

            // [START analyticsdata_json_credentials_run_report]
            // Initialize request argument(s)
            RunReportRequest request = new RunReportRequest
            {
                Property = "properties/" + propertyId,
                Dimensions = {
                    //new Dimension { Name = "city" },
                    //new Dimension { Name = "region" },
                    //new Dimension { Name = "country" },
                    new Dimension { Name = "language" },
                    //new Dimension { Name = "browser" },
                    //new Dimension { Name = "newVsReturning" },
                    //new Dimension { Name = "deviceCategory" },
                },
                Metrics = {
                    //new Metric { Name = "totalUsers" },
                    //new Metric { Name = "activeUsers" },
                },
                DateRanges = { new DateRange { StartDate = "2020-03-31", EndDate = "today" }, },
            };




            // Make the request
            //var response = client.RunReport(request);
            var response = client.BatchRunReports(new BatchRunReportsRequest
            {
                Property = "properties/" + propertyId,
                Requests =
                {
                    new RunReportRequest {
                        Dimensions = { new Dimension { Name = "country" }, new Dimension { Name = "region" }, new Dimension { Name = "city" }  },
                        Metrics = { new Metric { Name = "totalUsers" }, new Metric { Name = "activeUsers" } },
                         DateRanges = { new DateRange { StartDate = "2000-01-01", EndDate = "today" }, }
                    },

                    new RunReportRequest {
                        Dimensions = { new Dimension { Name = "language" }  },
                        Metrics = { new Metric { Name = "totalUsers" }, new Metric { Name = "activeUsers" } },
                       DateRanges = { new DateRange { StartDate = "2000-01-01", EndDate = "today" }, }
                    },

                    new RunReportRequest {
                        Dimensions = { new Dimension { Name = "browser" }  },
                        Metrics = { new Metric { Name = "totalUsers" }, new Metric { Name = "activeUsers" } },
                        DateRanges = { new DateRange { StartDate = "2000-01-01", EndDate = "today" }, }
                    },

                    new RunReportRequest {
                        Dimensions = { new Dimension { Name = "deviceCategory" }  },
                        Metrics = { new Metric { Name = "totalUsers" } },
                       DateRanges = { new DateRange { StartDate = "2000-01-01", EndDate = "today" }, }
                    },

                     new RunReportRequest {
                         Metrics = { new Metric { Name = "totalUsers" }, new Metric { Name = "activeUsers" } },
                        DateRanges = { new DateRange { StartDate = "2000-01-01", EndDate = "today" }, }
                    },

                    // new RunReportRequest {
                    //     Metrics = { new Metric { Name = "activeUsers" } },
                    //    DateRanges = { new DateRange { StartDate = "2020-03-31", EndDate = "today" }, }
                    //}
                },
            });
            // [END analyticsdata_json_credentials_run_report]
            string str = "";
            foreach (var report in response.Reports)
            {

                str = str + $"<tr><td> >>> Dimension [ {String.Join(", ", report.DimensionHeaders.Select(s => s.Name).ToArray())} ]";
                str = str + $"<tr><td>>>> Metric [ {String.Join(", ", report.MetricHeaders.Select(s => s.Name).ToArray())} ]]</td></tr>";
                foreach (var rw in report.Rows)
                {
                    str = str + $"<tr><td> {String.Join(", ", rw.DimensionValues.Select(s => s.Value).ToArray())}  ## {String.Join(", ", rw.MetricValues.Select(s => s.Value).ToArray())}</td></tr>";
                }
                str = str + "<tr><td>---------------------------------------------------</tr></td>";
            }
            return  str;
            // [START analyticsdata_json_credentials_run_report_response]
            //Console.WriteLine("Report result:");

            //foreach (var row in response.Rows)
            //{

            //    Console.WriteLine(
            //        $"{row.DimensionValues[0].Value}, {row.MetricValues[0].Value}, {row.MetricValues[0].Value}");
            //}
            // [END analyticsdata_json_credentials_run_report_response]
            //Console.ReadKey();
        }
    }
}