### How to run kubernetes

1. helm repo add bitnami https://charts.bitnami.com/bitnami
2. helm install my-release bitnami/kafka

3. kubectl apply -f mssql-plat-depl.yaml
4. kubectl apply -f local-pvc.yaml
5. kubectl apply -f twittor-depl.yaml
6. kubectl apply -f twittorlog-depl.yaml
7. kubectl apply -f twittordal-depl.yaml

### Build docker file

1. docker build -t rahmatalhakam/twittorservice .
2. docker push rahmatalhakam/twittorservice
3. docker build -t rahmatalhakam/twittorlog .
4. docker push rahmatalhakam/twittorlog
5. docker build -t rahmatalhakam/twittordal .
6. docker push rahmatalhakam/twittordal
