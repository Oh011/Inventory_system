namespace Shared.Parameters
{
    public class PaginationQueryParameters
    {




        private const int MaxPageSize = 10;


        private const int MinPageSize = 5;



        private int _PageSize { get; set; } = MaxPageSize;

        private int _PageIndex { get; set; } = 1;


        public int PageIndex
        {


            get { return _PageIndex; }


            set
            {

                if (value > 0)
                    _PageIndex = value;

            }

        }


        public int pageSize
        {
            get { return _PageSize; }
            set
            {


                if (value > MaxPageSize) _PageSize = MaxPageSize;

                else _PageSize = value;

            }



        }

    }
}
