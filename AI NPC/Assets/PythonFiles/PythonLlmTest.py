import sys
import time

def main():
    while True:
        try:
            # 입력 받기
            line = sys.stdin.readline().strip()
            if not line:
                continue
            
            # 두 정수 처리
            nums = list(map(int, line.split()))
            if len(nums) != 2:
                print("Error: Expected two integers", flush=True)
                continue

            # 3초 대기
            # time.sleep(3)
            
            result = sum(nums)
            print(f"Result: {result}", flush=True)


        except Exception as e:
            print(f"Error: {e}", flush=True)

if __name__ == "__main__":
    main()
