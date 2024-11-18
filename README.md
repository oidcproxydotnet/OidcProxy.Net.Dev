![kuberise logo](kuberise.io-OidcProxy.Net.png)
# kuberise.io & OidcProxy.Net

OidcProxy is a free, open source, community driven, developer-friendly, and secure BFF (Backend for Frontend) framework for ASP.NET Core. Designed specifically to host Single Page Applications, OidcProxy simplifies implementing end-user authentication with minimal configuration.

kuberise.io is a free open source internal developer platform for Kubernetes environment. The goal is to provide tools and templates in Kubernetes environment by a fast and easy installation to help developers focus on the development of the business applications rather than installation and configuration of side tools and preparations of the environments and automation.

The goal for this repository is to install all tools and services including identity servers in a local kubernetes cluster to make a full ready environment for development of OidcProxy.Net project. For more details read [kuberise.io](https://kuberise.io)

## Prerequisites

- CLI tools: kubectl, helm, htpasswd, git, openssl
- A Github account or another git repository system
- [K9s](https://k9scli.io/topics/install/) for dashboard (recommended)
- A [kind](https://kind.sigs.k8s.io/docs/user/quick-start#installation) kubernetes cluster for local installation (`kind create cluster`) or any other kubernetes cluster
- [cloud-provider-kind](https://github.com/kubernetes-sigs/cloud-provider-kind) for loadBalancer services and ingresses in kind cluster.

## Installation

1. Fork this repository [https://github.com/oidcproxydotnet/OidcProxy.Net.Dev](https://github.com/oidcproxydotnet/OidcProxy.Net.Dev) into your Github account.
2. Clone your new repository in your local computer and enter to the root folder.
3. Run this command:
```bash
./scripts/install.sh [CONTEXT] [NAME] [REPO_URL] [REVISION] [DOMAIN] [TOKEN]
```
- [CONTEXT] This is your kubernetes context. You can find your current kubernetes context by running this command: `kubectl config current-context` . If you install local kubernetes cluster using `minikube start` command, then your context would be `minikube` .
- [NAME] This is the name of your platform. For this name there should be a values-[NAME].yaml in app-of-apps folder and also a [NAME] folder in values folder for all configurations. If you choose oidcproxy, then you don't need to rename files and folders already exist in the repository.
- [REPO_URL] This is the url of your forked repository.
- [REVISION] This is the branch or commit sha or tag of the commit that you want to use for this installation. For example you can write "main" to deploy from the main branch.
- [DOMAIN] This is the domain for the cluster. All platform services and applications would be subdomain of this domain, for example: keycloak.[DOMAIN]. If you are deploying into minikube you can choose minikube.kuberise.dev for the domain then your keycloak address would be keycloak.minikube.kuberise.dev
- [TOKEN] If you are pushing this code to a private repository, you have to put a token here so the ArgoCD can access your repository. If your repository is public, skip this parameter.

Example: If you deployed a kubernetes cluster using `minikube start` and your platform name is `oidcproxy` then this would be the installation command:
```bash
./scripts/install.sh minikube oidcproxy https://github.com/oidcproxydotnet/OidcProxy.Net.Dev.git main minikube.kuberise.dev
```

For more information please read documentations here: [kuberise.io](https://kuberise.io)
