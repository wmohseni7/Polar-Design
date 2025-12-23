# Guide d’installation et de réutilisation – Polar Design

Ce guide détaille les procédures pour **installer**, **exécuter** et **compiler** le projet *Polar Design* à partir des sources.

---

## Prérequis techniques

Pour le développement ou la compilation, les éléments suivants sont nécessaires :

| Composant | Spécification |
| :--- | :--- |
| **Unity Hub** | Version 3.0 ou supérieure |
| **Unity Editor** | **2022.3.x LTS** (Long Term Support) |
| **Espace Disque** | ~2 Go (Assets + Librairies) |
| **OS Supportés** | Windows 10/11, macOS (Intel/M1/M2), Linux |

---

##  Exécution rapide (Utilisateur)

Si vous souhaitez uniquement tester le jeu, un exécutable pré-compilé est déjà inclus dans ce dépôt :

1. **Cloner le dépôt** :
   ```bash
   git clone https://git.unistra.fr/t3-milkalcoholics/t3-sci25b-t3-a
   ```

2. **Lancer le jeu :**

    Ouvrez le dossier racine du dépôt.

    Localisez et lancez l'exécutable nommé PolarDesign ( .exe sous Windows ou .app sous macOS).

## Installation et Développement développeur

### Étape 1 — Prérequis

* Installer **Unity Hub**
* Installer une version **Unity LTS compatible**
* Installer les modules de build nécessaires :

### Étape 2 — Récupération du projet

```bash
git clone https://git.unistra.fr/t3-milkalcoholics/t3-sci25b-t3-a
```

* Ouvrir **Unity Hub**
* Cliquer sur **Open Project**
* Sélectionner le dossier racine du projet

### Étape 3 — Préparation du build

* Ouvrir le projet dans Unity
* Vérifier l’absence d’erreurs critiques dans la **Console**
* Aller dans **File → Build Settings**
* Ajouter les scènes nécessaires :

  * Loading
  * UI
  * Towers
  * Rooms

### Étape 4 — Génération de l’exécutable

* Sélectionner la plateforme cible
* Cliquer sur **Build**
* Choisir un dossier de destination

Une fois le processus terminé, Unity génère :

Un exécutable> > PolarDesign. 

.exe sous Windows

.app sous macOS

Un dossier de données associé

Lancer l’exécutable pour démarrer le jeu sans Unity.
---

## 2. Dépannage (FAQ)
❌ **Les clics ne répondent pas**

Vérifier qu’un EventSystem est présent :

dans la scène active

ou dans la UIScene

❌ **La barre de satisfaction reste à zéro**

Vérifier que le GameSessionManager (Prefab) est bien présent

Vérifier qu’il est correctement instancié au démarrage

❌ **Erreurs de compilation**

Vérifier que :

Tous les scripts du dossier /Scripts sont importés

Aucun script référencé par un ScriptableObject n’est manquant

Les références dans l’Inspector sont correctement assignées

## 3. Réutilisation pédagogique et expérimentale

Ce guide permet à tout développeur, enseignant ou étudiant de :

Reprendre le projet Polar Design

Modifier les mécaniques ou les paramètres de game design

Générer une version exécutable autonome

Le projet peut ainsi être utilisé dans un cadre :

pédagogique

scientifique

expérimental