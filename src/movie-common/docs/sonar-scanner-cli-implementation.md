# Sonarqube Integration to General Project in GitLab

> This project guides you to integrate Sonarqube to a general project through GitLab CI/CD.
> This integration works with languages that does not ship with a compiler (i.e. HTML, PHP, and Ruby)
>
> All supported languages listed [here][supported-language].

## What is Sonarqube ?

SonarQube (formerly Sonar) is open source platform developed by SonarSource for continuous inspection of code quality.
It performs automatic reviews with static analysis of code to detect bugs, code smells, and security vulnerabilities on 20+ programming languages.
SonarQube offers reports on duplicated code, coding standards, unit tests, code coverage, code complexity, comments, bugs, and security vulnerabilities.

For more information about sonarqube, follow this [**link**][documentation].

## Installation

Below are steps to set up sonar analysis jobs

1. Add / Modify `.gitlab-ci.yml` with sonar analysis job definition
   > reference file placed on root folder of this project

### Job Details

| Name          | description                                             | execution trigger      |
| ------------- | ------------------------------------------------------- | ---------------------- |
| sonar-changes | Analyze changed file and report to gitlab               | `merge_requests`       |
| sonar-global  | Analyze project globally and update report in sonarqube | `develop` and `master` |

2. Set CI-CD Variable for project sonar analysis

### CI/CD Variables

| key              | description                                        | example/default value      |
| ---------------- | -------------------------------------------------- | -------------------------- |
| SONAR_AUTH_TOKEN | Sonarqube Access Token. [details][access-token]    | asd76erdz97b69rzed         |
| SONAR_HOST_URL   | Access url where sonar can be accessed from runner | https://gatesonar.agate.id |

1. Invite SonarQube `@sonarqube` in project membership as Developer
   ![sonarqube user](readme-img/img.png)

   > Sonarqube will open discussion and report any issue found in analysis result

2. (Optional) Configure file exclusions and inclusions if needed (excluding third party plugin) [details][file-exclusion]

## Further Reading

- [Sonarqube docs][documentation]

[documentation]: https://docs.sonarqube.org/latest/
[access-token]: https://docs.sonarqube.org/latest/user-guide/user-token/
[file-exclusion]: https://docs.sonarqube.org/latest/project-administration/narrowing-the-focus/
