#include <stdio.h>
#include <stdlib.h>

int main(){
    FILE *nFile, *mFile;
    int numOfNums = 10000;
	int high1 = 101;
	int high2 = 101;
	int x, genNum;
    char c;
    mFile = fopen("mFile", "w+");
    if(mFile != NULL){
        for(x = 0; x < numOfNums; x++){
			genNum = rand() % high1;
			fprintf(mFile, "%d ", genNum);
        }
    }
    fclose(mFile);

	nFile = fopen("nFile", "w+");
	if(nFile != NULL){
		for(x = 0; x < numOfNums; x++) {
			genNum = rand() % high2;
			fprintf(nFile, "%d ", genNum);
		}
	}
    return 0;
}

