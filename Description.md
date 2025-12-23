# ****Documentation pédagogique et technique – Polar Design****

## **1. Vision du projet**

**Polar Design** est un simulateur pédagogique visant à sensibiliser à l’importance du design environnemental dans des conditions de vie extrêmes (ICE – Isolé, Confiné, Extrême). En prenant pour cadre la base polaire Concordia, le projet confronte l’utilisateur à des dilemmes d’aménagement où l’esthétique doit répondre à des besoins psychologiques vitaux.

## **Objectifs pédagogiques**

**Comprendre les liens entre l’espace d’habitation et le bien-être psychologique :** analyser comment l’agencement architectural, les volumes et les choix esthétiques influencent le moral et la santé mentale des occupants.

**Identifier les enjeux de la conception architecturale en milieu extrême :** appréhender l’importance d’un aménagement intérieur adapté dans des environnements hostiles où les habitants passent la majeure partie de leur temps en intérieur.

**Appréhender la psychologie architecturale :** analyser comment les formes et volumes influencent les sensations de contrôle, d’harmonie ou d’oppression.

**Comprendre l’impact de la lumière sur le rythme circadien :** évaluer les effets des différents types d’éclairage sur le sommeil, l’éveil et le bien-être en l’absence de lumière naturelle.

**Sensibiliser aux limites d’une architecture pensée pour une population unique :** mettre en évidence les enjeux liés au genre, aux différences culturelles et aux usages dans la conception d’espaces partagés en bases polaires.

**Expérimenter la conception participative :** prendre en compte les besoins, préférences et retours des habitants afin de proposer des choix architecturaux cohérents avec leurs attentes.

**Explorer les principes de la psychologie des couleurs :** comprendre l’impact émotionnel des couleurs sur le moral (couleurs apaisantes, stimulantes ou oppressantes) et leur perception variable selon le contexte saisonnier antarctique.




## **2. Mécaniques de gameplay & feedback**

Le jeu adopte un système de point & click, reposant sur une boucle de rétroaction continue (feedback loop) entre les actions du joueur et les indicateurs de satisfaction.

### **Déroulement général du gameplay**

Le joueur débute sur un menu d’accueil

Il sélectionne ensuite une tour (Calm ou Noisy)

Puis choisit un étage et une pièce à concevoir

Une fois dans la pièce, le joueur interagit exclusivement par clic avec les éléments de l’environnement.

### **Interaction dans la pièce**

Le joueur clique directement sur les éléments 3D (mobilier).

Les options de personnalisation apparaissent dans le MenuPanel

Il peut alors choisir entre différentes configurations disponibles

#### **Éléments modifiables**

Mobilier : sélection de variantes influençant l’usure, le goût et l’adéquation avec les personnages

Couleurs des murs : choix via des boutons dans le MenuPanel

Éclairage : Intensité lumineuse

Température de couleur (lumière chaude ou froide)

### **Système de satisfaction et feedback**

Chaque modification met à jour en temps réel :

la satisfaction de la pièce

la satisfaction globale de l’étage

Ces valeurs sont affichées via des barres de satisfaction dynamiques

La satisfaction d’une pièce dépend de :

l’usure des éléments choisis

le goût des personnages

la nationalité et les préférences culturelles des occupants

### **Progression et changement de scène**

Un bouton permet de changer de pièce

Un panneau de confirmation demande au joueur s’il souhaite finaliser la conception de la pièce

Une fois validée, le joueur passe à une autre pièce de l’étage

Ce processus se répète jusqu’à ce que toutes les pièces de l’étage soient terminées

## **3.Connaissances à transmettre**

### **3.1 Relation entre espace construit et bien-être psychologique**

#### Compréhension du lien direct entre :
aménagement intérieur, confort psychologique, moral des occupants

#### Mise en évidence du rôle de l’architecture dans la gestion :
du stress, du confinement, de l’isolement social

#### Influence de l’ergonomie sur :
la fatigue mentale, la sensation de bien-être au quotidien

### **3.2 Psychologie des couleurs en milieu intérieur**
**Compréhension des effets émotionnels des couleurs :** couleurs apaisantes (vert, bleu), couleurs stimulantes (orange, jaune), couleurs oppressantes ou tristes (rouge, gris)

#### Prise en compte du contexte extrême antarctique :
alternance nuit polaire / jour continu, modification de la perception des couleurs selon la saison

### **3.3 Impact de la lumière sur le rythme circadien**
Rôle de la lumière artificielle en l’absence de lumière naturelle

#### Différence entre :
lumière chaude → relaxation, respect du sommeil
lumière froide → stimulation, inhibition de la mélatonine

#### Compréhension des effets à long terme sur :
le sommeil, l’humeur et la vigilance

### **3.5 Conception architecturale inclusive et participative**

**Importance de concevoir des espaces adaptés à :**

différents genres, différentes cultures et différentes nationalités
Mise en évidence des limites d’une architecture pensée pour une population unique

#### Sensibilisation aux risques de tensions sociales liées à : 
des choix de design inadaptés, l’absence de concertation des usagers

### **3.6 Notions de compromis et de décision systémique**

Compréhension que : optimiser une pièce ne garantit pas la satisfaction globale

#### Apprentissage de la prise de décision systémique :

arbitrage entre esthétique, confort et contraintes
vision globale à l’échelle de l’étage et de la tour

## **4. Architecture technique & design patterns**

Le projet repose sur des patrons de conception éprouvés, garantissant la maintenabilité, l’extensibilité et la clarté du code.

### **4.1 Strategy & Factory – Navigation et chargement des scènes**

Utilisation dans le projet

Le choix d’une tour déclenche une SceneLoaderFactory

La factory instancie la stratégie correspondante (CalmStrategy ou NoisyStrategy)

Chaque stratégie définit l’ordre et la liste des scènes accessibles

Avantages

Découplage : le système de navigation ne connaît pas les scènes concrètes

Extensibilité : ajout d’une nouvelle tour sans modifier le code existant

Flexibilité : modification de l’ordre des pièces sans dépendre de la hiérarchie Unity

### **4.2 Pattern Command – Système audio et actions**

Utilisation dans le projet

Toutes les actions sonores sont encapsulées sous forme de commandes

L’AudioManager agit comme invocateur central

Avantages

Centralisation : contrôle global du volume et des sources audio

Réutilisabilité : une commande PlayClickSound est utilisée partout

Performances : gestion efficace du pooling audio, évitant les allocations inutiles

### **4.3 Pattern Observer – Synchronisation UI / scène 3D**

Utilisation dans le projet

L’EventManager diffuse les changements (couleur, lumière, score)

Les objets 3D (murs, sols, meubles) s’abonnent aux événements

Avantages

Aucune dépendance directe entre l’UI et les objets 3D

Robustesse : réduction des erreurs de type NullReferenceException

Évolutivité : ajout de nouveaux observateurs sans modifier le code existant

### **4.4 Gestion des données – ScriptableObjects**

Les paramètres de game design sont externalisés :

ColorProfile

LightIntensityProfile

L’équilibrage du jeu se fait sans modifier le code.

Bénéfices

Ajustements rapides

Meilleure collaboration entre développeurs et designers

Approche orientée données

### **5. Parcours utilisateur (User Flow)**

Initialisation : scène de chargement (LoadingScene)

Sélection : choix de la tour et de l’étage via le NavigationManager

Édition : aménagement pièce par pièce

Suivi : le GameSessionManager conserve la progression

Finalisation : bilan global de satisfaction avant passage à l’étage suivant

### **6. Justification des choix techniques**

Unity URP : compromis entre performances et gestion avancée de la lumière (crucial pour le cycle circadien)

New Input System : compatibilité future et gestion propre des entrées

Navigation par pile (Stack) : gestion intuitive du bouton « Retour » sans rechargements inutiles

## **Conclusion**

L’alliance de ces patterns transforme une application de décoration en une architecture logicielle robuste, capable de supporter des environnements complexes et une évolution rapide du contenu pédagogique.

