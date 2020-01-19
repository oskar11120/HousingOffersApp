library(readxl)

dataIE <- read.csv("~/AGH/dataIE.csv", row.names=1, sep=";")
summary(dataIE)
prze_il <- function(wektor){
  wynik = wektor/sqrt(sum(wektor^2))
}
dataIE<- dataIE[,-c(2,4,5,6,7,8)]
dataIE_norm <- matrix(NA,nrow(dataIE),ncol(dataIE))
for(i in 1:4){
  dataIE_norm[,i] = prze_il(dataIE[,i])  
}
#ideal means that in the ranking cheap properties and properties in city centre (small value of "area") will be scored higher. Also properties with
#higher number of "clicks" (when user clicks to obtain contact to property owner) and "views" (when user click on advertisement to see details)
#means that advertisement will be scored higher
#anty is opposition of it
ideal <- c(min(dataIE_norm[,1]),min(dataIE_norm[,2]),max(dataIE_norm[,3]),
           max(dataIE_norm[,4]))
anty <- c(max(dataIE_norm[,1]),max(dataIE_norm[,2]),min(dataIE_norm[,3]),
          min(dataIE_norm[,4]))
dataIE_norm

OI <-matrix(NA,nrow(dataIE),1)
for(i in 1:nrow(dataIE_norm)){
  ob = dataIE_norm[i,]
  I = ideal
  ob_rozszerz = rbind(ob, I)
  OI[i] = dist(ob_rozszerz)
}
OI
OA <- matrix(NA,nrow(dataIE_norm),1)
for(i in 1:nrow(dataIE_norm)){
  ob = dataIE_norm[i,]
  A = anty
  ob_rozszerz = rbind(ob, A)
  OA[i] = dist(ob_rozszerz)
}
OA
Tscore<-matrix(NA,nrow(dataIE_norm),1)
for (i in 1:nrow(dataIE_norm)){
  Tscore[i]<- OA[i]/(OI[i]+OA[i])
}
Tscore
ranking <- matrix(NA,nrow(dataIE_norm),2)
ranking
ranking[,1]<- rownames(dataIE)
for(i in 1:nrow(dataIE)){
  
  ranking[i,2] <- Tscore[i]
  
}
ranking
colnames(ranking) = c("ID", "TS")
ranking<-ranking[order(ranking[,2],ranking[,1],decreasing=TRUE),]
write.csv(ranking, file = 'AGH/rank.csv', row.names = FALSE)
