#  Copyright (C) 2019 Tim van Vugt
#
#  This program is free software: you can redistribute it and/or modify
#  it under the terms of the GNU Affero General Public License as published by
#  the Free Software Foundation, either version 3 of the License, or
#  (at your option) any later version.
#
#  This program is distributed in the hope that it will be useful,
#  but WITHOUT ANY WARRANTY; without even the implied warranty of
#  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#  GNU Affero General Public License for more details.
#
#  You should have received a copy of the GNU Affero General Public License
#  along with this program.  If not, see <https://www.gnu.org/licenses/>.

import time
import subprocess
import os
import re
from datetime import datetime
from dateutil import tz
from termcolor import colored

print(colored("P2000 Message Receiver", 'yellow') + " - " + colored("(c) nltimv 2019", 'blue', attrs=['bold']))

groupidold = ""


def curtime():
    return time.strftime("%H:%M:%S %Y-%m-%d")


with open('error.txt', 'a') as file:
    file.write(('#' * 20) + '\n' + curtime() + '\n')

multimon_ng = subprocess.Popen("rtl_fm -f 169.65M -M fm -s 22050 -p 43 -g 30 | multimon-ng -a FLEX -t raw -",
                               stdout=subprocess.PIPE,
                               stderr=open('error.txt', 'a'),
                               shell=True)

print(colored("Ready to receive P2000 messages", 'green'))

try:
    while True:
        line = multimon_ng.stdout.readline()
        output = line.decode("utf-8")
        multimon_ng.poll()
        if "ALN" in output and output.startswith("FLEX"):

            flex = output[0:5]
            timestamp = output[6:25]
            melding = output[60:]
            groupid = output[37:43]
            capcode = output[45:54]

            regex_prio1 = "^A\s?1|\s?A\s?1|PRIO\s?1|^P\s?1"
            regex_prio2 = "^A\s?2|\s?A\s?2|PRIO\s?2|^P\s?2"
            regex_prio3 = "^B\s?1|^B\s?2|^B\s?3|PRIO\s?3|^P\s?3|PRIO\s?4|^P\s?4"

            if re.search(regex_prio1, melding, re.IGNORECASE):
                priokleur = 'red'

            elif re.search(regex_prio2, melding, re.IGNORECASE):
                priokleur = 'yellow'

            elif re.search(regex_prio3, melding, re.IGNORECASE):
                priokleur = 'green'

            else:
                priokleur = 'magenta'

            if groupid == groupidold:

                print(colored(capcode, 'white')),

            else:

                utc = datetime.strptime(timestamp, '%Y-%m-%d %H:%M:%S')
                utc = utc.replace(tzinfo=tz.tzutc())
                local = utc.astimezone(tz.tzlocal())
                local = local.strftime("%d-%m-%Y %H:%M:%S")

                print(' ')
                print(colored(local, 'blue', attrs=['bold']), colored(melding, priokleur, attrs=['bold'])),
                print('                  '),
                print(colored(capcode, 'white')),

                groupidold = groupid


except KeyboardInterrupt:
    os.kill(multimon_ng.pid, 9)
