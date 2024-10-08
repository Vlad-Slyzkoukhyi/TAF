HOME TASK
The following actions should be performed as part of this task:
•	CI tool should be set up for execution (Azure DevOps Pipelines)
•	Pipeline should be created, capable of executing tests with the following triggers (Pipelines as Code approach is strongly preferred):
•	On pull request to branch
•	By schedule
•	By manual start
•	The whole suite of tests should run on pipeline as follows:
•	API and UI tests should run on separate steps of pipeline execution with API being first
•	If API tests fail, UI tests should still be executed
•	All results and screenshots taken (if any) should be published as artifacts at the end of pipeline run
User should be able to select browser to run UI tests against when manually triggering the run.

Implementation strategy:
UI tests:
1) Create Page objects - create separate classes for different pages and initialaze page emelements in this classes;
2) Create utilities classes - for clear understanding what this feature do;
3) Create Assertions class - where implement all methods for asser expected result;
4) Create configuration json to configure global settings and create class for this configuration.
5) Implement Test class - where configure driver, configuration and create all needed test cases;

Api tests:
1) Create business layers with coresponded atributes;
2) Create request and response builders;
3) configure settings;
4) Create tests;

Pipeline:
Configure pipeline;