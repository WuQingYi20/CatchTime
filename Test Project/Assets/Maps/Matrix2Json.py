import json
import csv

matrix = []

nameMap = {
    0:'无',
1: '书包',
2: '椅子',
3: '长椅子',
4: '新椅子',
5: '讲台',
6:'桌子',
7:'长桌子',
8: '书架',
9:'机房凳子',
10:'盆栽',

}


data = {"Items":[],
        "total":0}
if __name__ == "__main__":
    with open('Map1.csv','r', encoding='utf-8') as file:
        reader = csv.reader(file)

        for line in reader:
            matrix.append(list(int(l) for l in line))

    for x,line in enumerate(matrix):
        for z,number in enumerate(line):
            if number != 0:
                data['total'] += 1
                data['Items'].append({
                    "id":number,
                    'name':nameMap[number],
                    'relpos_x':x,
                    'relpos_z':z,
                    'angle':0
                })
    with open('Map1.json','w',encoding='utf-8') as jsonfile:
        json.dump(data,jsonfile,ensure_ascii=False)
