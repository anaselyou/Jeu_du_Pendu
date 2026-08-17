// Main
bool rejouer;

do
{
    Console.Clear();
    bool etat = JeuDuPendu(); // true = victoire, false = defaite

    if (etat)
        EcrireTexte(0, 23, "GG vous avez gagné !");
    else
        EcrireTexte(0, 23, "Vous avez perdu !");

    EcrireTexte(0, 24, "Voulez-vous rejouer ? (O/N) : ");

    string reponse = Console.ReadLine();

    rejouer = reponse.ToUpper() == "O";
} while (rejouer);


// Verifie qu'un caractere est une lettre (maj ou min) non accentuee
static bool EstUneLettre(char verificationLettre)
{
    return (verificationLettre >= 'A' && verificationLettre <= 'Z' ||
            verificationLettre >= 'a' && verificationLettre <= 'z');
}

// Convertit une lettre minuscule en majuscule, sinon renvoie le caractere tel quel
static char LettreEnMajuscule(char lettre)
{
    if (!EstUneLettre(lettre))
    {
        return lettre;
    }
    else
        return char.ToUpper(lettre);
}

// Saisie bloquante d'une lettre valide (redemande tant que la saisie est incorrecte)
static char SaisirLettre()
{
    char lettre;
    bool valide;
    EcrireTexte(0, 22, ("Veuillez taper une lettre non accentué suivie de <ENTER> "));
    do
    {
        valide = true;
        lettre = Console.ReadLine()[0];
        if (!EstUneLettre(lettre))
        {
            EcrireTexte(0, 22, ("Saisi invalide ... Recommencez "));
            valide = false;
        }
    } while (!valide);
    return LettreEnMajuscule(lettre);
}

// Verifie qu'un mot ne contient que des lettres (utilise pour valider la saisie du mot de reference)
static bool EstComposeDeLettres(string mot)
{
    for (int i = 0; i < mot.Length; i++)
    {
        if (!EstUneLettre(mot[i]))
            return false;
    }
    return true;
}

static string MotEnMajuscule(string mot)
{
    if (EstComposeDeLettres(mot))
    {
        return mot.ToUpper();
    }

    return "";
}

// Saisie bloquante d'un mot valide (redemande tant qu'il contient un caractere non-lettre)
static string SaisirMot()
{
    string mot;
    bool valide;


    EcrireTexte(0, 22, ("Veuillez taper un mot à trouver suivi de <ENTER> "));
    do
    {

        mot = Console.ReadLine();
        if (!EstComposeDeLettres(mot))
        {
            EcrireTexte(0, 22, ("Saisi invalide ... Recommencez "));
            valide = false;
        }
        else valide = true;
    } while (!valide);
    return MotEnMajuscule(mot);
}

// Construit le mot masque de depart (que des points, un par lettre)
static string CacherMot(string motInitialiser)
{
    string motCacher = "";
    for (int i = 0; i < motInitialiser.Length; i++)
    {
        motCacher += ".";
    }

    return motCacher;
}

// Verifie si une lettre proposee apparait dans le mot de reference
static bool EstDansMot(char lettreProposer, string mot)
{
    for (int i = 0; i < mot.Length; i++)
    {
        if ((lettreProposer) == mot[i])
        {
            return true;
        }
    }

    return false;
}

// Devoile les occurrences de la lettre proposee dans le mot masque
static string CompleterMot(string motACompleter, string motReference, char lettreProposer)
{
    string resultat = "";
    for (int i = 0; i < motReference.Length; i++)
    {
        if (motReference[i] == lettreProposer)
        {
            resultat += motReference[i];
        }
        else
        {
            resultat += motACompleter[i];
        }
    }

    return resultat;
}

// Dessine une partie du pendu selon le nombre d'erreurs deja commises (1 a 6)
static void AfficherPendu(int nbrErreur)
{
    switch (nbrErreur)
    {
        case 1:
            DessinerTete(84, 5);
            break;
        case 2:
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(85, 7 + i);
                Console.Write("*");
            }

            break;
        case 3:
            LigneDiagonaleDroite(86, 10, 4);
            break;
        case 4:
            LigneDiagonaleGauche(84, 10, 4);
            break;
        case 5:
            LigneDiagonaleDroite(86, 16, 5);
            break;
        case 6:
            LigneDiagonaleGauche(84, 16, 5);
            break;
    }


}

// Dessine la potence et la corde, affichees en permanence des le debut de la partie
static void AfficherPotence()
{
    LigneHorizontale(85, 2, 12);

    for (int i = 0; i < 20; i++)
    {
        Console.SetCursorPosition(96, 3 + i);
        Console.Write("*");
    }
    LigneDiagonaleDroite(92, 3, 3);
    for (int i = 0; i < 2; i++)
    {
        Console.SetCursorPosition(85, 3 + i);
        Console.Write("*");
    }
}

static void LigneHorizontale(int posH, int posV, int longeur)
{


    for (int i = 0; i < longeur; i++)
    {
        // On place le curseur au point de départ (x = position, y = ligne)
        Console.SetCursorPosition(posH + i, posV);
        Console.Write('*');
    }
}

static void LigneDiagonaleGauche(int posH, int posV, int longeur)
{
    for (int i = 0; i < longeur; i++)
    {
        // On place le curseur au point de départ (x = position, y = ligne)
        Console.SetCursorPosition(posH - i, posV + i);
        Console.Write('*');
    }
}

static void LigneDiagonaleDroite(int posH, int posV, int longeur)
{
    Console.SetCursorPosition(posH, posV);

    for (int i = 0; i < longeur; i++)
    {
        Console.SetCursorPosition(posH + i, posV + i);
        Console.Write('*');
    }
}

static void DessinerTete(int posH, int posV)
{
    LigneHorizontale(posH, posV, 3);
    Console.SetCursorPosition(posH, posV + 1);
    Console.Write("*");

    Console.SetCursorPosition(posH + 2, posV + 1);
    Console.Write("*");

    LigneHorizontale(posH, posV + 2, 3);
}

// Efface la ligne y avant d'y ecrire le texte, pour eviter les caracteres parasites d'un affichage precedent
static void EcrireTexte(int x, int y, string texte)
{
    Console.SetCursorPosition(x, y);

    Console.Write(new string(' ', Console.WindowWidth));

    Console.SetCursorPosition(x, y);

    Console.Write(texte);
}

// Joue une partie complete du pendu, renvoie true si le joueur gagne, false s'il perd
static bool JeuDuPendu()
{
    Console.Clear();
    AfficherPotence();
    int count = 0;

    Console.SetCursorPosition(0, 22);
    string motReference = SaisirMot();
    string motCacher = CacherMot(motReference);
    string lettreIncorrectes = "";
    char lettreChoisi;

    while (true)
    {
        lettreChoisi = SaisirLettre();
        motCacher = CompleterMot(motCacher, motReference, lettreChoisi);

        // On ne compte l'erreur que si cette lettre incorrecte n'a pas deja ete proposee
        if (!EstDansMot(lettreChoisi, motReference))
        {
            if (!lettreIncorrectes.Contains(lettreChoisi))
            {
                count++;
                AfficherPendu(count);
                lettreIncorrectes += lettreChoisi + " ";
            }
        }

        EcrireTexte(0, 0, ("Mot : " + motCacher));

        EcrireTexte(25, 0, ("Lettres incorrectes : " + lettreIncorrectes));

        if (count == 6)
        {
            EcrireTexte(0, 23, ("Vous avez perdu fin de partie"));
            return false;
        }

        if (!motCacher.Contains("."))
        {
            EcrireTexte(0, 23, ("GG gagné !"));
            return true;
        }
    }
}