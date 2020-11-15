//CSC 410 Big List Assignment
//Sequential version

#include <omp.h>
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>

void printArray(int array[], int length) {
	if(length > 30){
		length = 30;
	}
	for(int k = 0; k < length; k++){
		printf("%3d", array[k]);
	}
	printf("\n");
}

int min(int a, int b){
	if(a < b){
		return a;
	}
	return b;
}

int findDivisor(int a, int b) {
	int divisor;
	int result;
	divisor = min(a, b);
	while(divisor >= 1){
		if((a % divisor == 0 || a % divisor == 1) \
			&& (b % divisor == 0 || b % divisor == 1)){
			return divisor;
		}
		divisor--;
	}
}

int main(int argc, char *argv[])
{
	int arrLength, k;
	char *mFileName, *nFileName, *resultFileName;
	FILE *mFile, *nFile, *resultFile;
	mFileName = "mFile";
	nFileName = "nFile";
	resultFileName = "resultFile";

	//Get length from command line or set as constant
	if(argc < 2){
		arrLength = 10;
	} else {
		arrLength = atoi(argv[1]);
		if(arrLength < 1){
			arrLength = 10;
		}
	}
	printf("length is %d\n", arrLength);

	int M[arrLength], N[arrLength], P[arrLength], Q[arrLength], R[arrLength];

	//Input M and N from files
	mFile = fopen(mFileName, "r");
	if(mFile != NULL){
		for(k = 0; k < arrLength; k++) {
			fscanf(mFile, "%d", &M[k]);
		}
		fclose(mFile);
		printf("\n\nM :: ");
		printArray(M, arrLength);
	} else {
		printf("M file didn't open! \n");
		return -1;
	}

	nFile = fopen(nFileName, "r");
	if(nFile != NULL){
		for(k = 0; k < arrLength; k++) {
			fscanf(nFile, "%d", &N[k]);
		}
		fclose(nFile);

		printf("\n\nN ::  ");
		printArray(N, arrLength);
	} else {
		printf("N file didn't open! \n");
		return -1;
	}

	resultFile = fopen(resultFileName, "w+");
	if(resultFile != NULL) {
		//P is the difference between Mk and Nk



		#pragma omp parallel for
		for(k = 0; k < arrLength; k++) {
			P[k] = abs(M[k] - N[k]);
		}	//fprintf(resultFile, "%d ", P[k]);



		for(k=0; k<arrLength; k++){
			fprintf(resultFile,"%d ",P[k]);

		}

		printf("\n________________________________________________________________________________________\n\nP :: ");
		printArray(P, arrLength);
		fputc('\n', resultFile);

		//Q is the largest value that divides Mk and Nk 
		//less than the max of Mk, Nk with a remainder of 0 or 1

		#pragma omp parallel for
		for(k = 0; k < arrLength; k++){
			Q[k] = findDivisor(M[k], N[k]);
			//fprintf(resultFile, "%d ", Q[k]);
		}


		for(k=0; k  < arrLength; k++){

			fprintf(resultFile,"%d ",Q[k]);

		}

		printf("\n_______________________________________________________________________________________\n\nQ :: ");
		printArray(Q, arrLength);
		fputc('\n', resultFile);

		//R is the largest values that divides Mk and Nk
		//less than the value of Mk and Pk with a remainder of 0 or 1

		#pragma omp parallel for
		for(k = 0; k < arrLength; k++) {
			R[k] = findDivisor(M[k], P[k]);
			//fprintf(resultFile, "%d ", R[k]);
		}


		for(k=0; k< arrLength; k++){

			fprintf(resultFile,"%d ",R[k]);
		}

		printf("\n_______________________________________________________________________________________\n\nR :: ");
		printArray(R, arrLength);
		fputc('\n', resultFile);

		fclose(resultFile);
	} else {
		printf("Result file failed to open! \n");
	}
	return 0;
}
