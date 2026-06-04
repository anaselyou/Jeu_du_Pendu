
static bool EstUneLettre(char verificationLettre)
{
    return (verificationLettre >= 'A' && verificationLettre <= 'Z' ||
            verificationLettre >= 'a' && verificationLettre <= 'z');
}

static char LettreEnMajuscule(char lettre)
{
    if (!EstUneLettre(lettre))
    {
        return lettre;
    }
    else
        return char.ToUpper(lettre);
}

static char SaisirLettre()
{
    char lettre;
    bool valide;
    EcrireTexte(0,22,("Veuillez taper une lettre non accentué suivie de <ENTER> "));
    do
    {
        valide = true;
        lettre = Console.ReadLine()[0];
        if (!EstUneLettre(lettre))
        {
            EcrireTexte(0,22,("Saisi invalide ... Recommencez "));
            valide = false;
        }
    } while (!valide);
    return LettreEnMajuscule(lettre);
}

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

//Saisir un mot de référence
static string SaisirMot()
{
    string mot;
    bool valide;
    

    EcrireTexte(0,22,("Veuillez taper un mot á trouver suivi de <ENTER> "));
    do
    {
        
        mot = Console.ReadLine();
        if (!EstComposeDeLettres(mot))
        {
            EcrireTexte(0,22,("Saisi invalide ... Recommencez "));
            valide = false;
        }
        else valide = true;
    } while (!valide);
    return MotEnMajuscule(mot);
}

static string CacherMot(string motInitialiser)
{
    string motCacher = "";
    for (int i = 0; i < motInitialiser.Length; i++)
    {
        motCacher += ".";
    }

    return motCacher;
}

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
            LigneDiagonaleDroite(86,10,4);
            break;
        case 4:
            LigneDiagonaleGauche(84,10,4);
            break;
        case 5:
            LigneDiagonaleDroite(86,16,5);
            break;
        case 6:
            LigneDiagonaleGauche(84,16,5);
            break;
    }
    

}

static void AfficherPotence()
{
    LigneHorizontale(85, 2, 12);

    for (int i = 0; i < 20; i++)
    {
        Console.SetCursorPosition(96,3 + i);
        Console.Write("*");
    }
    LigneDiagonaleDroite(92, 3, 3);
    for (int i = 0; i < 2; i++)
    {
        Console.SetCursorPosition(85,3 + i);
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

static void EcrireTexte(int x, int y, string texte)
{
    Console.SetCursorPosition(x, y);

    Console.Write(new string(' ', Console.WindowWidth));

    Console.SetCursorPosition(x, y);

    Console.Write(texte);
}

static void JeuDuPendu()
{
    Console.Clear();
    AfficherPotence();
    bool gagne, perdu;
    int count = 0;
    
    Console.SetCursorPosition(0, 22);
    string motReference = SaisirMot();
    string motCacher = CacherMot(motReference);
    string lettreIncorrectes = "";
    char lettreChoisi;
    
    do
    {
        lettreChoisi = SaisirLettre();
        motCacher = CompleterMot(motCacher, motReference, lettreChoisi);

        if (!EstDansMot(lettreChoisi, motReference))
        {   
            count++;
            AfficherPendu(count);

            if (!lettreIncorrectes.Contains(lettreChoisi))
                lettreIncorrectes += lettreChoisi + " ";
            
        }
        
        EcrireTexte(0,0,("Mot : " + motCacher));

        EcrireTexte(25,0,("Lettres incorrectes : " + lettreIncorrectes));
        
        perdu = false;
        gagne = false;
        
        if (count == 6)
        {
            EcrireTexte(0,23,("Vous avez perdu fin de partie"));
            perdu = true;
        }

        if (!motCacher.Contains("."))
        {
            EcrireTexte(0,23,("GG gagné !"));
            gagne = true;
        }
    } while (!gagne && !perdu);
}

// Main
Console.Clear();
JeuDuPendu();


