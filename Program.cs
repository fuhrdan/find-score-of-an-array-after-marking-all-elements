//*****************************************************************************
//** 2593. Find Score of an Array After Marking All Elements    leetcode     **
//*****************************************************************************

// Define a structure for priority queue elements
typedef struct {
    int value;
    int index;
} Element;

// Comparison function for priority queue (min-heap)
int compare(const void* a, const void* b) {
    Element* elemA = (Element*)a;
    Element* elemB = (Element*)b;
    if (elemA->value == elemB->value) {
        return elemA->index - elemB->index;
    }
    return elemA->value - elemB->value;
}

long long findScore(int* nums, int numsSize) {
    bool* vis = (bool*)calloc(numsSize, sizeof(bool));

    Element* pq = (Element*)malloc(numsSize * sizeof(Element));
    if (!pq) {
        fprintf(stderr, "Memory allocation failed for pq\n");
        free(vis);
        return -1;
    }

    int pqSize = 0;

    for (int i = 0; i < numsSize; i++) {
        pq[pqSize++] = (Element){nums[i], i};
    }

    qsort(pq, pqSize, sizeof(Element), compare);

    long long ans = 0;
    int heapIndex = 0;

    while (heapIndex < pqSize) {
        Element top = pq[heapIndex++];

        if (vis[top.index]) {
            continue;
        }

        ans += top.value;
        vis[top.index] = true;

        if (top.index + 1 < numsSize) {
            vis[top.index + 1] = true;
        }
        if (top.index - 1 >= 0) {
            vis[top.index - 1] = true;
        }

        while (heapIndex < pqSize && vis[pq[heapIndex].index]) {
            ++heapIndex;
        }
    }

    free(vis);
    free(pq);

    return ans;
}
