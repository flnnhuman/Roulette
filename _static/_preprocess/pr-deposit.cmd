SET prefix=css\deposit

SET src=%prefix%\deposit.css
SET targ=%prefix%\deposit.out.css
SET var=%prefix%\var.txt

python %proc% -s %src% -t %targ% -v %var%

SET src=%prefix%\deposit.common.css
SET targ=%prefix%\deposit.common.out.css
SET var=%prefix%\var.txt

python %proc% -s %src% -t %targ% -v %var%
