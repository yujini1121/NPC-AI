# pip3 install python-dotenv

import sys
import time
import os
from langchain.chains import LLMChain
from langchain_core.output_parsers import StrOutputParser
from langchain_core.prompts import PromptTemplate
from langchain_community.llms import OpenAI
from langchain_openai import ChatOpenAI
from dotenv import load_dotenv

print(f"Python : ===START===", flush=True)
print(f"Python : Hello!", flush=True)
load_dotenv('key.env')
print(f"Python : env received", flush=True)

envkey = os.getenv("OPENAI_API_KEY")
print(f"Python : setted Key", flush=True)

llm = ChatOpenAI(
    model="gpt-4o-mini",
    api_key=envkey
    )
template = PromptTemplate.from_template("""
You are given an input string from a user: {user_input}
Your task is to interpret the user's intention based on the input and describe the user's intention as a clear sentence.
""")

def llm_test(user_input):
    output_parser = StrOutputParser()

    chain = template | llm | output_parser

    history_npc_intent = chain.invoke({"user_input" : user_input})
    return history_npc_intent

def main():
    while True:
        try:
            # 입력 받기
            line = sys.stdin.readline()
            if not line:
                continue
            print(f"Python received line.", flush=True)
            # 3초 대기
            # time.sleep(3)

            llm_answer = llm_test(line)
            
            print(f"Result: {llm_answer}", flush=True)


        except Exception as e:
            print(f"Error: {e}", flush=True)

if __name__ == "__main__":
    main()
