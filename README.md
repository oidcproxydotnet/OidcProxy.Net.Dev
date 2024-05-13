# kuberise

kuberise is a free opensource internal developer platform for Kubernetes environment.

## Prerquisites

- kubectl,helm,htpasswd,git command line tools
- git repository

## Installation

### Summary

- fork the repo in your github account (or clone the project and push it in any other code repository)
- provide a value file in app-of-apps folder with the name of your cluster
- run cicd/scripts/install.sh command with all required input parameters

### Installation details
tbd
Assume you have one management cluster that you deploy your platform services in this cluster and then you have separate kubernetes clusters for your applications and for different environment and this management cluster will provide platform services for all development environments.

<!-- TODO: Complete the instruction for multiple management clusters   -->
### Multiple management clusters
What if you want to have multiple management cluster for different environment. for example one management cluster

## How to uninstall
tbd
```
tbd
```

## Platform Engineering Concept

Being a DevOps today is different than before. In the complex environment of different cloud providers and Kubernetes clusters, it is challenging to be a developer and also know how to deploy your application to these environments.

Platform teams can create an internal developer platform to abstract all the complexities of the deployment environments from the developers and help them to focus on their development. They provide a set of self-service tools, templates, best defaults and support to developers to be able to deploy their application whenever they want and to any environment they want without need to know and learn all details of the Kubernetes or different tools.

After deployment of their applications, developers can also monitor and maintain their own application themselves. kuberise can help platform engineers or developers to create an internal developer platform for their teams.

## kuberise features

kuberise will deploy several tools to provide a developer environment in your Kubernetes cluster.
- You can choose which tool you want to install
- Are tools are common open source projects that are popular in IT environments.
- There is no lock-in in kuberise. After deployment of kuberise, you will have the full control of your environment and shape your environment to fit you best.
- kuberise is built based on GitOps best practices. The repository is the only source of truth and you can track the changes and avoid any manual changes in the cluster.

## kuberise tools

These tools are currently included in kuberise and more tools will be included in the future:

### CD (Continuous Deployment)
- ArgoCD
### Observability
- Grafana
- Loki
- Prometheus
- Promtail

### Authentication and Authorisation and security
- Keycloak
- sealed-secret
- cert-manager
### Data
- PostgreSQL

### Networking
- Ingress-nginx
