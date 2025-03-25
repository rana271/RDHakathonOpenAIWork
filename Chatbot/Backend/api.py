from fastapi import FastAPI, HTTPException
from openai import OpenAI
import os
from dotenv import load_dotenv
from Data.process import vector_store  # Absolute import

# Load environment variables
load_dotenv()
api_key = os.getenv("OPENAI_API_KEY")
client = OpenAI(api_key=api_key)

app = FastAPI()

def call_ai_model(query, context):
    prompt = f"""Based on this context:\n{context}\n
    Extract and return the following in a structured format:
    - Incident ID (if present)
    - RCA (Root Cause Analysis)
    - Solution (if present)
    - Priority (e.g., P1, P2, P3, P4)
    If any field is missing, return 'Not found'. Answer in JSON format."""
    response = client.chat.completions.create(
        model="gpt-3.5-turbo",
        messages=[
            {"role": "system", "content": "You are a support chatbot for a company's platform team."},
            {"role": "user", "content": prompt}
        ],
        max_tokens=200,
        temperature=0.5
    )
    return response.choices[0].message.content

@app.get("/chat")
async def chat(query: str):
    if not query.strip():
        raise HTTPException(status_code=400, detail="Query cannot be empty")
    
    docs = vector_store.similarity_search(query, k=3)
    context = "\n".join([doc.page_content for doc in docs])
    
    if not context:
        return {
            "incident_id": "Not found",
            "rca": "No relevant data found",
            "solution": "Not found",
            "priority": "Not found"
        }
    
    try:
        response = call_ai_model(query, context)
        import json
        structured_response = json.loads(response)
        return structured_response
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Error processing query: {str(e)}")