import PyPDF2
from langchain.text_splitter import RecursiveCharacterTextSplitter
from langchain.embeddings import OpenAIEmbeddings
from langchain.vectorstores import FAISS
import os
from dotenv import load_dotenv

# Load environment variables
load_dotenv()
api_key = os.getenv("OPENAI_API_KEY")

# Extract text from PDFs
def extract_text_from_pdfs(directory):
    all_text = ""
    for filename in os.listdir(directory):
        if filename.endswith(".pdf"):
            with open(os.path.join(directory, filename), "rb") as file:
                reader = PyPDF2.PdfReader(file)
                for page in reader.pages:
                    all_text += page.extract_text() + "\n"
    return all_text

# Index the data
pdf_directory = os.path.join(os.path.dirname(__file__), "Documents")
pdf_text = extract_text_from_pdfs(pdf_directory)
text_splitter = RecursiveCharacterTextSplitter(chunk_size=1000, chunk_overlap=200)
chunks = text_splitter.split_text(pdf_text)
embeddings = OpenAIEmbeddings(api_key=api_key)
vector_store = FAISS.from_texts(chunks, embeddings)