# Gestion de Compte
Kata réalisé dans le cadre d'un entretien technique

## Enoncé
Features : Indiquer la valeur d’un compte pour une date donnée
* Input : une date entre le 1 er janvier 2022 et 1 Mars 2023
* Output : Afficher la valeur du compte en euros.

Le code doit être évolutif, afin de pouvoir créer facilement de nouvelles fonctionnalités.
Le but n’étant pas d’avoir un projet fini, mais bien fait (prévoir jusqu’à 1h)


## Contraintes techniques
* Le livrable doit être fournis via un gestionnaire de code de votre choix (exemple GIT, TFS
…)
* Langage C#

Description du fichier Account.csv  
Compte au 28/02/2023 (dd/MM/yyyy) : 8300.00 EUR =&gt; La date courante et la situation actuelle  
EUR/JPY : 0.482 =&gt; taux de change yen vers euro  
EUR/USD : 1.445 =&gt; taux de change dollar vers euro  
Puis une liste de transaction entre le 1 er Janvier 2022 et 1 er Mars 2023
```
Date;Montant;Devise;Catégorie
06/10/2022;-504.61;EUR;Loisir
```

Les transactions peuvent être en trois devises (euro : EUR, yen : JPY, dollar : USD).

A partir du fichier csv fourni, justifier les 3 plus grandes catégories de débit sur tout l’historique:
```
Habitation
Alimentation
Santé
```