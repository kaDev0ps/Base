#===== The beginning of the Copyright Notice =====
copyright()
{

echo -e "
'============================== The Beginning of the Copyright Notice ==========================================================
' The AUTHOR of this file is Alexander Borisovich Prokopyev born on December 20, 1977 resident of the city of Kurgan, Russia;	
' Series and Russian passport number (only the last two digits for each one): **22-****91					
' Russian Individual Taxpayer Number of the AUTHOR (only the last four digits): ********2007					
' Russian Insurance Number of Individual Ledger Account of the AUTHOR (only the last five digits): ***-***-859 04
' Copyright (C) Alexander B. Prokopyev, 2023, All Rights Reserved.
' Contact: 	a.prokopyev.resume at gmail dot com
'
' All source code contained in this file is protected by copyright law.
' This file is available under AGPL v3 (GNU Affero General Public License): https://www.gnu.org/licenses/agpl-3.0.en.html
' PROVIDED FOLLOWING RESTRICTIONS APPLY:
' Nobody except the AUTHOR may alter or remove this copyright notice from any legal copies of this file content.
' Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an
' \"AS IS\" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
' specific language governing permissions and limitations under the License.
'
' ATTENTION: If your country laws are not compatible or collide with this license terms you are prohibited to use this content.
'================================= The End of the Copyright Notice =============================================================
";

}

CallCount=0;

operand()
{
        Position=$1;
        Type=$2;
        echo -ne "$Position $Type called. \t";
        (( CallCount++ ));
	case $Type in
		( "false" )
			return 1;
		;;
		( "true" )
			return 0;
		;;
		(*)
			echo "Incorrect operand type!";
			exit 2;
		;;
	esac;
}

operation()
{
	Op=$1
	V1=$2;
	V2=$3;
	CallCount=0;
	echo -ne "===>: $V1 $Op $V2: \t  ";
	eval operand "left $V1" $Op operand "right $V2"; Result=$?; if [ $CallCount == 1 ]; then echo -ne "\t\t\t"; fi; echo -n "Exit code: $Result";
	echo;
	exit $Result;
}

test_matrix()
{
	Op=$1;
	operation $Op "false" "false";
	operation $Op "false" "true";
	operation $Op "true" "false";
	operation $Op "true" "true";
}

test_matrix2()
{
        Op=$1;
        operation $Op "false" "false"; 
        operation $Op "false" "true";
        operation $Op "true" "false";
        operation $Op "true" "true";
}

test_matrix "||";
test_matrix "&&";
