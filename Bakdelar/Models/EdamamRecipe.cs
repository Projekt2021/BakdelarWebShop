using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Models.External
{
    public class EdamamRecipe
    {
        public string q { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public bool more { get; set; }
        public int count { get; set; }
        public Hit[] hits { get; set; }
    }

    public class Hit
    {
        public Recipe recipe { get; set; }
        public bool bookmarked { get; set; }
        public bool bought { get; set; }
    }

    public class Recipe
    {
        public string uri { get; set; }
        public string label { get; set; }
        public string image { get; set; }
        public string source { get; set; }
        public string url { get; set; }
        public string shareAs { get; set; }
        public float yield { get; set; }
        public string[] dietLabels { get; set; }
        public string[] healthLabels { get; set; }
        public string[] cautions { get; set; }
        public string[] ingredientLines { get; set; }
        public Ingredient[] ingredients { get; set; }
        public float calories { get; set; }
        public float totalWeight { get; set; }
        public float totalTime { get; set; }
        public string[] cuisineType { get; set; }
        public string[] mealType { get; set; }
        public string[] dishType { get; set; }
        //public Totalnutrients totalNutrients { get; set; }
        //public Totaldaily totalDaily { get; set; }
        //public Digest[] digest { get; set; }
    }

    //public class Totalnutrients
    //{
    //    public ENERC_KCAL ENERC_KCAL { get; set; }
    //    public FAT FAT { get; set; }
    //    public FASAT FASAT { get; set; }
    //    public FATRN FATRN { get; set; }
    //    public FAMS FAMS { get; set; }
    //    public FAPU FAPU { get; set; }
    //    public CHOCDF CHOCDF { get; set; }
    //    public FIBTG FIBTG { get; set; }
    //    public SUGAR SUGAR { get; set; }
    //    public SUGARAdded SUGARadded { get; set; }
    //    public PROCNT PROCNT { get; set; }
    //    public CHOLE CHOLE { get; set; }
    //    public NA NA { get; set; }
    //    public CA CA { get; set; }
    //    public MG MG { get; set; }
    //    public K K { get; set; }
    //    public FE FE { get; set; }
    //    public ZN ZN { get; set; }
    //    public P P { get; set; }
    //    public VITA_RAE VITA_RAE { get; set; }
    //    public VITC VITC { get; set; }
    //    public THIA THIA { get; set; }
    //    public RIBF RIBF { get; set; }
    //    public NIA NIA { get; set; }
    //    public VITB6A VITB6A { get; set; }
    //    public FOLDFE FOLDFE { get; set; }
    //    public FOLFD FOLFD { get; set; }
    //    public FOLAC FOLAC { get; set; }
    //    public VITB12 VITB12 { get; set; }
    //    public VITD VITD { get; set; }
    //    public TOCPHA TOCPHA { get; set; }
    //    public VITK1 VITK1 { get; set; }
    //    public WATER WATER { get; set; }
    //}

    //public class ENERC_KCAL
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class FAT
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class FASAT
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class FATRN
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class FAMS
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class FAPU
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class CHOCDF
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class FIBTG
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class SUGAR
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class SUGARAdded
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class PROCNT
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class CHOLE
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class NA
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class CA
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class MG
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class K
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class FE
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class ZN
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class P
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class VITA_RAE
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class VITC
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class THIA
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class RIBF
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class NIA
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class VITB6A
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class FOLDFE
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class FOLFD
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class FOLAC
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class VITB12
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class VITD
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class TOCPHA
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class VITK1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class WATER
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class Totaldaily
    //{
    //    public ENERC_KCAL1 ENERC_KCAL { get; set; }
    //    public FAT1 FAT { get; set; }
    //    public FASAT1 FASAT { get; set; }
    //    public CHOCDF1 CHOCDF { get; set; }
    //    public FIBTG1 FIBTG { get; set; }
    //    public PROCNT1 PROCNT { get; set; }
    //    public CHOLE1 CHOLE { get; set; }
    //    public NA1 NA { get; set; }
    //    public CA1 CA { get; set; }
    //    public MG1 MG { get; set; }
    //    public K1 K { get; set; }
    //    public FE1 FE { get; set; }
    //    public ZN1 ZN { get; set; }
    //    public P1 P { get; set; }
    //    public VITA_RAE1 VITA_RAE { get; set; }
    //    public VITC1 VITC { get; set; }
    //    public THIA1 THIA { get; set; }
    //    public RIBF1 RIBF { get; set; }
    //    public NIA1 NIA { get; set; }
    //    public VITB6A1 VITB6A { get; set; }
    //    public FOLDFE1 FOLDFE { get; set; }
    //    public VITB121 VITB12 { get; set; }
    //    public VITD1 VITD { get; set; }
    //    public TOCPHA1 TOCPHA { get; set; }
    //    public VITK11 VITK1 { get; set; }
    //}

    //public class ENERC_KCAL1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class FAT1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class FASAT1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class CHOCDF1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class FIBTG1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class PROCNT1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class CHOLE1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class NA1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class CA1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class MG1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class K1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class FE1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class ZN1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class P1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class VITA_RAE1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class VITC1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class THIA1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class RIBF1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class NIA1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class VITB6A1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class FOLDFE1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class VITB121
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class VITD1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class TOCPHA1
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    //public class VITK11
    //{
    //    public string label { get; set; }
    //    public float quantity { get; set; }
    //    public string unit { get; set; }
    //}

    public class Ingredient
    {
        public string text { get; set; }
        public float weight { get; set; }
        public string foodCategory { get; set; }
        public string foodId { get; set; }
        public string image { get; set; }
    }

    //public class Digest
    //{
    //    public string label { get; set; }
    //    public string tag { get; set; }
    //    public string schemaOrgTag { get; set; }
    //    public float total { get; set; }
    //    public bool hasRDI { get; set; }
    //    public float daily { get; set; }
    //    public string unit { get; set; }
    //    public Sub[] sub { get; set; }
    //}

    //public class Sub
    //{
    //    public string label { get; set; }
    //    public string tag { get; set; }
    //    public string schemaOrgTag { get; set; }
    //    public float total { get; set; }
    //    public bool hasRDI { get; set; }
    //    public float daily { get; set; }
    //    public string unit { get; set; }
    //}

}
