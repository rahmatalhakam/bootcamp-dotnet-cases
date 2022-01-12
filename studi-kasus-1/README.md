### How to run kubernetes

1. kubectl apply -f mssql-plat-depl.yaml
2. kubectl apply -f local-pvc.yaml
3. kubectl apply -f auth-depl.yaml
4. kubectl apply -f payment-depl.yaml
5. kubectl apply -f enrollment-np-srv.yaml
6. kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.0.3/deploy/static/provider/cloud/deploy.yaml
7. kubectl apply -f ingress-srv.yaml

### How to bild docker image

1. docker build -t rahmatalhakam/paymentservice .
2. docker push rahmatalhakam/paymentservice
3. docker build -t rahmatalhakam/authservice .
4. docker push rahmatalhakam/authservice
5. docker build -t rahmatalhakam/enrollmentservice .
6. docker push rahmatalhakam/enrollmetservice
