# 이 파일은 유니티에서 열지 말고 그냥 IDLE에서 실행하세요

import os
from langchain.chains import LLMChain
from langchain_core.output_parsers import StrOutputParser
from langchain_core.prompts import PromptTemplate
from langchain_community.llms import OpenAI
from langchain_openai import ChatOpenAI
from dotenv import load_dotenv

load_dotenv('key.env')

#os.environ["OPENAI_API_KEY"] = os.getenv("OPENAI_API_KEY")
envkey = os.getenv("OPENAI_API_KEY")
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

llm_answer = llm_test("Hi, how are you doing these days?")

print(f"answer : {llm_answer}")
