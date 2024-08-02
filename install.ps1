$context = kubectl config current-context
$name = "oidcproxy-dev-env"
$repoUrl = "https://github.com/oidcproxydotnet/OidcProxy.Net.Dev.git"
$revision = "main"
$domain = "minikube"
$token = ""

./scripts/install.sh $context $name $repoUrl $revision $domain $token

minikube tunnel