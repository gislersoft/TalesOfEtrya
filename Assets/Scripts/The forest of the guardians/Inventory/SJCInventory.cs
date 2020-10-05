using System.Linq;
public class SJCInventory : Inventory {

    #region Singleton

    new public static SJCInventory instance;

    protected override void Awake()
    {
        //base.Awake();
        if(instance == null)
        {
            instance = this;
        }
    }

    #endregion

    public int[] lettersAcquired;
    public int coinsAcquired;

    public void Start()
    {
        if(instance == null)
        {
            lettersAcquired = new int[26];
            coinsAcquired = 0;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddLetter(Letter letter)
    {
        switch (letter.letter)
        {
            #region Add letter counter
            case 'A':
                lettersAcquired[(int)Letters.A] += 1;
                break;
            case 'B':
                lettersAcquired[(int)Letters.B] += 1;
                break;
            case 'C':
                lettersAcquired[(int)Letters.C] += 1;
                break;
            case 'D':
                lettersAcquired[(int)Letters.D] += 1;
                break;
            case 'E':
                lettersAcquired[(int)Letters.E] += 1;
                break;
            case 'F':
                lettersAcquired[(int)Letters.F] += 1;
                break;
            case 'G':
                lettersAcquired[(int)Letters.G] += 1;
                break;
            case 'H':
                lettersAcquired[(int)Letters.H] += 1;
                break;
            case 'I':
                lettersAcquired[(int)Letters.I] += 1;
                break;
            case 'J':
                lettersAcquired[(int)Letters.J] += 1;
                break;
            case 'K':
                lettersAcquired[(int)Letters.K] += 1;
                break;
            case 'L':
                lettersAcquired[(int)Letters.L] += 1;
                break;
            case 'M':
                lettersAcquired[(int)Letters.M] += 1;
                break;
            case 'N':
                lettersAcquired[(int)Letters.N] += 1;
                break;
            case 'O':
                lettersAcquired[(int)Letters.O] += 1;
                break;
            case 'P':
                lettersAcquired[(int)Letters.P] += 1;
                break;
            case 'Q':
                lettersAcquired[(int)Letters.Q] += 1;
                break;
            case 'R':
                lettersAcquired[(int)Letters.R] += 1;
                break;
            case 'S':
                lettersAcquired[(int)Letters.S] += 1;
                break;
            case 'T':
                lettersAcquired[(int)Letters.T] += 1;
                break;
            case 'U':
                lettersAcquired[(int)Letters.U] += 1;
                break;
            case 'V':
                lettersAcquired[(int)Letters.V] += 1;
                break;
            case 'W':
                lettersAcquired[(int)Letters.W] += 1;
                break;
            case 'X':
                lettersAcquired[(int)Letters.X] += 1;
                break;
            case 'Y':
                lettersAcquired[(int)Letters.Y] += 1;
                break;
            case 'Z':
                lettersAcquired[(int)Letters.Z] += 1;
                break;
                #endregion
        }
    }

    public void AddCoin()
    {
        coinsAcquired++;
    }

    public int TotalAmountOfLettersCollected()
    {
        return coinsAcquired;
    }

}

public enum Letters
{
    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J,
    K,
    L,
    M,
    N,
    O,
    P,
    Q,
    R,
    S,
    T,
    U,
    V,
    W,
    X,
    Y,
    Z
}
