# Перенос почтовых ящиков между серверами при помощи imapsync

<!-- Прежде всего откроем /etc/apt/sources.list и убедимся, что у репозиториев подключен раздел contrib, если это не так, то в каждую строку после -->

main

<!-- необходимо добавить -->

contrib

<!-- После чего не забываем обновить список пакетов: -->

apt update

<!-- После чего обращаемся на сайт разработчика за инструкциями по установке. Для Debian они выглядят следующим образом: -->

apt install \
 libauthen-ntlm-perl \
 libcgi-pm-perl \
 libcrypt-openssl-rsa-perl \
 libdata-uniqid-perl \
 libencode-imaputf7-perl \
 libfile-copy-recursive-perl \
 libfile-tail-perl \
 libio-socket-inet6-perl \
 libio-socket-ssl-perl \
 libio-tee-perl \
 libhtml-parser-perl \
 libjson-webtoken-perl \
 libmail-imapclient-perl \
 libparse-recdescent-perl \
 libproc-processtable-perl \
 libmodule-scandeps-perl \
 libreadonly-perl \
 libregexp-common-perl \
 libsys-meminfo-perl \
 libterm-readkey-perl \
 libtest-mockobject-perl \
 libtest-pod-perl \
 libunicode-string-perl \
 liburi-perl \
 libwww-perl \
 libtest-nowarnings-perl \
 libtest-deep-perl \
 libtest-warn-perl \
 make \
 time \
 cpanminus

 <!-- После того, как мы скачали все необходимые зависимости перейдем в домашнюю директорию и скачаем саму утилиту: -->

cd
wget -N https://raw.githubusercontent.com/imapsync/imapsync/master/imapsync

<!-- И сразу сделаем ее исполняемой: -->

chmod +x imapsync

<!-- В простейшем случае перенос ящика будет выглядеть так: -->

./imapsync \
 --host1 imap.yandex.ru \
 --user1 admin@snabavangard.ru \
 --password1 "sntfjxaitpepcsil" \
 --host2 m.snabavangard.ru \
 --user2 admin@snabavangard.ru \
 --password2 "Zel0bit04k@\_MAIL"

 <!-- Для исключения следует использовать опцию --exclude, которая поддерживает регулярные выражения. Скажем, уберем из синхронизации папку Спам и Корзину: -->

--exclude 'Spam|Trash'

<!-- Если вам нужно явно указать соответствие папок, то добавьте опцию: -->

--f1f2 Outbox=Sent

<!-- В данном случае мы указываем, что содержимое папки Outbox ящика-источника следует поместить в папку Sent ящика-приемника.

Еще одной полезной опцией является указание возраста писем, допустим мы хотим перенести корреспонденцию только за текущий год, не проблема, указываем: -->

--maxage 365

<!-- В итоге будут синхронизированы только письма не старше 365 дней.

А что делать с остальными? А можно перенести их в другой, архивный ящик, в этом нам поможет другая опция: -->

--minage 365

<!-- Теперь мы перенесем только письма с возрастом старше одного года.

Также эти опции можно комбинировать, они сочетаются по принципу И: -->

--maxage 730 --minage 365

<!-- Такая конструкция перенесет письма только за прошлый год (не старше двух лет и не моложе года).

А если указать наоборот? -->

--maxage 365 --minage 730

<!-- То мы перенесем все письма за текущий год, и те, которые старше двух лет (не старше 1 года и не моложе 2 лет). -->

<!-- С синтаксисом немного разобрались, но как быть, если ящиков много? Конечно же автоматизировать, для этого в официальной документации приведен пример скрипта: -->

#!/bin/sh
{ while IFS=';' read h1 u1 p1 h2 u2 p2 fake
do
./imapsync --host1 "$h1" --user1 "$u1" --password1 "$p1" \
   --host2 "$h2" --user2 "$u2" --password2 "$p2" \
 --automap --exclude 'Spam|Trash' --delete2 --delete2duplicates --delete2folders --syncinternaldates
done ;} < list.txt

<!-- Данный скрипт не блещет изысканными решениями и прост как табуретка. На его вход подается файл list.txt, который следует создать в одной директории со скриптом и из которого берутся адреса и учетные данные для узлов источника и приемника. Из опций указываем, что копируем с сохранением директорий, не копируем спам и корзину, удаляем письма, которых нет на сервере-источнике, удаляем дубли на сервере-приемнике - удаляем дубли папок и синхронизируем время писем с сервером-приемником. Сам файл file.txt должен содержать строки: -->

host1;user1_1;password11_1;host2;user2_1;password2_1;
host1;user1_2;password11_2;host2;user2_2;password2_2;
host1;user1_3;password11_3;host2;user2_3;password2_3;
host1;user1_4;password11_4;host2;user2_4;password2_4;

<!-- Дополнительные опции вы можете указать после "$@" или передать интерактивно при запуске скрипта, тогда они войдут в переменную $@. -->

m.snabavangard.ru;Mozhar.d@snabavangard.ru;Rz1Fs6nz;imap.yandex.ru;Mozhar.d@snabavangard.ru;rucxctpowslhpjis;
m.snabavangard.ru;101@snabavangard.ru;55XVf6fk;imap.yandex.ru;101@snabavangard.ru;kdgqjtfaumjzevys;
m.snabavangard.ru;102@snabavangard.ru;Hn295BYn;imap.yandex.ru;102@snabavangard.ru;qxhhnlrgjanismri;
m.snabavangard.ru;103@snabavangard.ru;34Azsx55;imap.yandex.ru;103@snabavangard.ru;tlfrikhkgsakiehg;
m.snabavangard.ru;104@snabavangard.ru;McT67Avx;imap.yandex.ru;104@snabavangard.ru;gfeliipxxzdopjpw;
m.snabavangard.ru;105@snabavangard.ru;nvU6xifS;imap.yandex.ru;105@snabavangard.ru;yeambgmhftmwcmoo;
m.snabavangard.ru;106@snabavangard.ru;Heb5Avcc;imap.yandex.ru;106@snabavangard.ru;clkymgalwxmachwg;
m.snabavangard.ru;107@snabavangard.ru;qbzFh3qj;imap.yandex.ru;107@snabavangard.ru;wtydzrdrkxzqaylw;
m.snabavangard.ru;108@snabavangard.ru;34Azsx56;imap.yandex.ru;108@snabavangard.ru;wklzrvjnnledrkxk;
m.snabavangard.ru;109@snabavangard.ru;19TaUwma;imap.yandex.ru;109@snabavangard.ru;ijryebdeumjddvkg;
m.snabavangard.ru;110@snabavangard.ru;56vsnb48;imap.yandex.ru;110@snabavangard.ru;tqfnxkzklbmorgch;
m.snabavangard.ru;111@snabavangard.ru;34Azsx56;imap.yandex.ru;111@snabavangard.ru;gksfcdlvkrgxzbwx;
m.snabavangard.ru;112@snabavangard.ru;0GfMlOMA1;imap.yandex.ru;112@snabavangard.ru;xbvkvhmxijasnvou;
m.snabavangard.ru;113@snabavangard.ru;41hsnb79;imap.yandex.ru;113@snabavangard.ru;ajjcwcimccplkxzd;
m.snabavangard.ru;114@snabavangard.ru;Versed!9;imap.yandex.ru;114@snabavangard.ru;kkpifewqaytwklhl;
m.snabavangard.ru;115@snabavangard.ru;sn3957zx;imap.yandex.ru;115@snabavangard.ru;dpwlzcmsviqaqaey;
m.snabavangard.ru;116@snabavangard.ru;1Usiqqp12;imap.yandex.ru;116@snabavangard.ru;vopmxtxfbrvlhcmd;
m.snabavangard.ru;117@snabavangard.ru;cNb4zxkN1;imap.yandex.ru;117@snabavangard.ru;etnvbnjbnowmuuyl;
m.snabavangard.ru;118@snabavangard.ru;mK1FMYZn;imap.yandex.ru;118@snabavangard.ru;zfduvezsvovbzsdn;
m.snabavangard.ru;119@snabavangard.ru;bcYW7bSW;imap.yandex.ru;119@snabavangard.ru;omftxnpotateqbpr;
m.snabavangard.ru;120@snabavangard.ru;D38HcJQq;imap.yandex.ru;120@snabavangard.ru;gtyxwbxfnsdzkxqk;
m.snabavangard.ru;121@snabavangard.ru;BdPaxnf1;imap.yandex.ru;121@snabavangard.ru;cngdcbecpfuxqrcf;
m.snabavangard.ru;122@snabavangard.ru;18cvyu95;imap.yandex.ru;122@snabavangard.ru;zqfygxqxtsvigvix;
m.snabavangard.ru;123@snabavangard.ru;39ashj58;imap.yandex.ru;123@snabavangard.ru;pcgcftkojqbgpkac;
m.snabavangard.ru;124@snabavangard.ru;34Azsx56;imap.yandex.ru;124@snabavangard.ru;bjdcijrnbdngprgs;
m.snabavangard.ru;125@snabavangard.ru;LmBThQcA;imap.yandex.ru;125@snabavangard.ru;eruzjzyeyjphjccb;
m.snabavangard.ru;126@snabavangard.ru;9X6ATkcQ;imap.yandex.ru;126@snabavangard.ru;rtwanpyxjeolxcue;
m.snabavangard.ru;127@snabavangard.ru;95snbf74;imap.yandex.ru;127@snabavangard.ru;psoyvpzujdjdbycs;
m.snabavangard.ru;128@snabavangard.ru;HZPqK22ai;imap.yandex.ru;128@snabavangard.ru;afdwpcqhwxyvchyp;
m.snabavangard.ru;129@snabavangard.ru;32gsnb76;imap.yandex.ru;129@snabavangard.ru;mkmjmfqdpnzttcmo;
m.snabavangard.ru;130@snabavangard.ru;64fsnb811;imap.yandex.ru;130@snabavangard.ru;yizpvlmmybuiwsrc;
m.snabavangard.ru;131@snabavangard.ru;865BjJ1L;imap.yandex.ru;131@snabavangard.ru;zznceeblzdqqclmz;
m.snabavangard.ru;132@snabavangard.ru;62qsnb542023;imap.yandex.ru;132@snabavangard.ru;eiixxywqrteczmgh;
m.snabavangard.ru;133@snabavangard.ru;N3Ttb1ec;imap.yandex.ru;133@snabavangard.ru;amneemzhoqmbqqbs;
m.snabavangard.ru;134@snabavangard.ru;25lsnb43;imap.yandex.ru;134@snabavangard.ru;amvpqbysmvjcooml;
m.snabavangard.ru;135@snabavangard.ru;25lsnb76;imap.yandex.ru;135@snabavangard.ru;flgylztqytzzihwn;
m.snabavangard.ru;136@snabavangard.ru;osnb3159;imap.yandex.ru;136@snabavangard.ru;wgzzkjkjptxytcbr;
m.snabavangard.ru;137@snabavangard.ru;26zsnb79;imap.yandex.ru;137@snabavangard.ru;ldpdjfmimjacvyoc;
m.snabavangard.ru;138@snabavangard.ru ;34Azsx56!;imap.yandex.ru;138@snabavangard.ru ;bzmyvggqazbfoxab;
m.snabavangard.ru;139@snabavangard.ru;Pogudkina_200;imap.yandex.ru;139@snabavangard.ru;eiujwjmquzisrwov;
m.snabavangard.ru;140@snabavangard.ru ;140_kazanov;imap.yandex.ru;140@snabavangard.ru ;xyehyicaxdkptngv;
m.snabavangard.ru;141@snabavangard.ru;DK0nqHLE;imap.yandex.ru;141@snabavangard.ru;uoihwbfsdkojbhwm;
m.snabavangard.ru;142@snabavangard.ru;sn4576ab;imap.yandex.ru;142@snabavangard.ru;cgcmfyayvosqhsfk;
m.snabavangard.ru;143@snabavangard.ru;jLeVyM58;imap.yandex.ru;143@snabavangard.ru;lociynvvkcddtttk;
m.snabavangard.ru;144@snabavangard.ru;84Azsx56;imap.yandex.ru;144@snabavangard.ru;dtmdtghosgjvazcu;
m.snabavangard.ru;145@snabavangard.ru;S1as1d2!;imap.yandex.ru;145@snabavangard.ru;alsvwctntgieqxac;
m.snabavangard.ru;info@snabavangard.ru;V7v9i8aEMOgp;imap.yandex.ru;info@snabavangard.ru;jdgstldxufjydeez;
m.snabavangard.ru;promo@snabavangard.ru;V7v9i8aEMOgp;imap.yandex.ru;promo@snabavangard.ru;kfylzdqmhcfzibic;
