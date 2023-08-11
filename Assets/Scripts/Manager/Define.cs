#region 오브젝트_데이터
[System.Serializable]
public class Product
{
    /// <summary>
    /// 품목명
    /// </summary>
    public string DESC_KOR;
    /// <summary>
    /// 총내용량
    /// </summary>
    public string SERVING_SIZE;
    /// <summary>
    /// 식품군
    /// </summary>
    public string GROUP_NAME;
    /// <summary>
    /// 제조사명
    /// </summary>
    public string MAKER_NAME;
    ///<summary>
    /// 가격
    ///</summary> 
    public int PRICE;

    /// <summary>
    /// 열량(kcal)(1회제공량당)
    /// </summary>
    public string NUTR_CONT1;
    /// <summary>
    /// 탄수화물(g)(1회제공량당)
    /// </summary>
    public string NUTR_CONT2;
    /// <summary>
    /// 단백질(g)(1회제공량당)
    /// </summary>
    public string NUTR_CONT3;
    /// <summary>
    /// 지방(g)(1회제공량당)
    /// </summary>
    public string NUTR_CONT4;
    /// <summary>
    /// 당류(g)(1회제공량당)
    /// </summary>
    public string NUTR_CONT5;
    /// <summary>
    /// 나트륨(mg)(1회제공량당)
    /// </summary>
    public string NUTR_CONT6;
    /// <summary>
    /// 콜레스테롤(mg)(1회제공량당)
    /// </summary>
    public string NUTR_CONT7;
    /// <summary>
    /// 포화지방산(g)(1회제공량당)
    /// </summary>
    public string NUTR_CONT8;
    /// <summary>
    /// 트랜스지방(g)(1회제공량당)
    /// </summary>
    public string NUTR_CONT9;
}
#endregion

public class ProductData
{
    public Product[] row;
}


#region  유저_데이터
[System.Serializable]
public class UserData
{
    public string name;
    public string phone;
    public string address;
    public string cardnum;
    public string exp;
    public string cvc;
    public string pin;

    public UserData() { }
    public UserData(string name, string phone, string address, string cardnum, string exp, string cvc, string pin)
    {
        this.name = name;
        this.phone = phone;
        this.address = address;
        this.cardnum = cardnum;
        this.exp = exp;
        this.cvc = cvc;
        this.pin = pin;
    }
}
#endregion

#region 상품 목록
public enum ProductName
{
    새우깡 = 0,
    허니버터칩,
    포카칩오리지널,
    하리보골드바렌,
    자일리톨껌,
    메로나,
    월드콘,
    투게더,
    토레타,
    밀키스,
    코카콜라,
    칠성사이다,
    매운치즈볶음밥,
    뉴스팸참치마요삼각김밥,
    전주비빔삼각김밥,
    신라면,
    진순,
    진매,
    컵뚝배기불고기밥,
    컵참치마요덮밥
}
#endregion